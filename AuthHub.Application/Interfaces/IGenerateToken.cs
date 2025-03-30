using AuthHub.Domain.Entities;

namespace AuthHub.Application.Interfaces
{
    public interface IGenerateToken
    {
        string GenerateJWT(User usuario);
    }
}
