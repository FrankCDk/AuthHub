using AuthHub.Application.Dtos.Response;
using AuthHub.Application.Interfaces;
using AuthHub.Application.Services.v1.Commands;
using AuthHub.Application.Validations.v1;
using AuthHub.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Data.Common;
using System.Net.Http;

namespace AuthHub.Application.Services.v1.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IHttpContextService _httpContextService;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterHandler(IMapper mapper, IUserRepository userRepository, IDatabaseConnectionFactory databaseConnectionFactory, IHttpContextService httpContextService, IPasswordHasher passwordHasher)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _databaseConnectionFactory = databaseConnectionFactory;
            _httpContextService = httpContextService;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validate = new RegisterCommandValidation();
            var resultado = validate.Validate(request);

            if (!resultado.IsValid)
            {
                //foreach (var item in resultado.Errors)
                //{
                //    Console.WriteLine($"{item.ErrorCode}-{item.ErrorMessage}");
                //}

                throw new Exception("Errores en la validación del usuario.");
            }

            if(request.Password != request.PasswordConfirmed)
            {
                throw new Exception("Las contraseñas deben ser iguales.");
            }

            User usuario = _mapper.Map<User>(request);

            //Hasheamos el password para almancenarlo
            var (hash, salt) = _passwordHasher.HashPassword(request.Password);

            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;

            //Obtenemos de la cabecera HTTP el tipo de base de datos que usaremos para conectarnos
            var databaseType = _httpContextService.GetHeaderValue("Database-Type") ?? throw new Exception("Conexión no enviada en la solicitud.");

            //Iniciamos la apertura de la conexion.
            using var cn = _databaseConnectionFactory.Connect(databaseType) as DbConnection;
            await cn!.OpenAsync(cancellationToken);
            bool execute = await _userRepository.Register(cn, databaseType, usuario);
            await cn.CloseAsync();

            if (!execute)
            {
                throw new Exception("No se logro registrar ningún usuario.");
            }
        }
    }
}
