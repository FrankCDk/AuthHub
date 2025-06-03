using AuthHub.Application.Auth.v1.Commands;
using AuthHub.Application.Dtos.Response;
using AuthHub.Application.Interfaces;
using AuthHub.Application.Validations.v1.Auth;
using AuthHub.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Data.Common;
using System.Net.Http.Headers;

namespace AuthHub.Application.Services.v1.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
    {

        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IGenerateToken _generateToken;
        private readonly IHistoryAccessRepository _historyAccessRepository;

        public LoginHandler(
            IUserRepository userRepository, 
            IMapper mapper, 
            IHttpContextService httpContextService, 
            IDatabaseConnectionFactory databaseConnectionFactory,
            IPasswordHasher passwordHasher,
            IGenerateToken generateToken,
            IHistoryAccessRepository historyAccessRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextService = httpContextService;
            _databaseConnectionFactory = databaseConnectionFactory;
            _passwordHasher = passwordHasher;
            _generateToken = generateToken;
            _historyAccessRepository = historyAccessRepository;
        }


        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar los datos enviados
            var validator = new LoginComandValidation();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                throw new Exception("Errores en la validación de los datos enviados.");
            }

            // 2. Mappear el Commmand
            User usuario = _mapper.Map<User>(request);

            // 3. Iniciar la transaccion
            var databaseType = _httpContextService.GetHeaderValue("Database-Type") ?? throw new Exception("Conexión no enviada en la solicitud.");
            using var cn = _databaseConnectionFactory.Connect(databaseType) as DbConnection;
            await cn!.OpenAsync(cancellationToken);

            // 4. Obtener el usuario
            User? user = await _userRepository.GetUser(cn, databaseType, usuario);

            //Validamos si existe el usuario
            if (user == null) throw new Exception("No se encontro el usuario.");
            //Validamos si el usuario esta activo
            if (user.Status != "A") throw new Exception("El usuario no esta Activo.");            
 
            // 5. Validar el password
            if(!_passwordHasher.VerifyPassword(usuario.PasswordHash, user.PasswordHash, user.PasswordSalt))
            {
                await cn.CloseAsync();
                throw new Exception("La contraseña es incorrecta.");
            }

            // 6. Generar token 
            string token = _generateToken.GenerateJWT(user);

            // 7. Registrarlo en el historial de accesos
            await _historyAccessRepository.Register(cn, databaseType, new HistoryAccess()
            {
                IdUser = user.Id,                
                FechaAcceso = DateTime.Now,
                Exito = true,
                Mensaje = "Login exitoso."
            });

            await cn.CloseAsync();

            // 8. Devolver el DTO con el Token
            LoginDto usuarioDto = new ()
            {
                Correo = user.Email,
                Usuario = user.Username,
                Token = token,
                Rol = user.Role.ToString(),
            };

            return usuarioDto;

        }
    }
}
