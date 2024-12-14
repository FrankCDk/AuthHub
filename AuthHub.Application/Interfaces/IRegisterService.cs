using AuthHub.Application.Dto.Register;

namespace AuthHub.Application.Interfaces
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
