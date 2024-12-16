using AuthHub.Application.Dto.Login;
using AuthHub.Application.Models;

namespace AuthHub.Application.Interfaces
{
    public interface ILoginService
    {
        Task<Result<LoginResponse>> Login(LoginRequest request);
    }
}
