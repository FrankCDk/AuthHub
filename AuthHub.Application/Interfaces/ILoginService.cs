using AuthHub.Application.Dto.Login;

namespace AuthHub.Application.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
