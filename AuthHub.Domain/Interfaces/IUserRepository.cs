using AuthHub.Domain.Entities;

namespace AuthHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Register(User user);
        Task<bool> VerifyCredentialsAsync(string username, string password);

    }
}
