using AuthHub.Application.Dto.Login;
using AuthHub.Application.Interfaces;
using AuthHub.Domain.Interfaces;

namespace AuthHub.Application.Services
{
    public class LoginService : ILoginService
    {

        private readonly IUserAuthenticationRepository _userAuthenticationRepository;

        public LoginService(IUserAuthenticationRepository userAuthenticationRepository)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {

            LoginResponse response = new LoginResponse();

            //Nos conectamos a la base de datos
            await _userAuthenticationRepository.VerifyCredentialsAsync(request.Username, request.Password);

            //Devolvemos la respuesta
            return response;

        }
    }
}
