using System.Data;

namespace AuthHub.Application.Interfaces
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection Connect(string dbType);
    }
}
