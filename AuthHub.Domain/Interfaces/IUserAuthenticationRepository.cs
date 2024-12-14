namespace AuthHub.Domain.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<bool> VerifyCredentialsAsync(string username, string password);
    }
}
