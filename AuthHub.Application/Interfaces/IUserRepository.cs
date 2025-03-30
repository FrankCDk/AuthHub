using AuthHub.Application.Dtos.Response;
using AuthHub.Domain.Entities;
using System.Data.Common;

namespace AuthHub.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(DbConnection connection, string dbType, User usuario);
        Task<bool> Register(DbConnection connection, string dbType, User usuario);
    }
}
