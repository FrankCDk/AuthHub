using MySql.Data.MySqlClient;
using System.Data.Common;

namespace AuthHub.Infrastructure.Interfaces
{
    public interface IDatabaseConnection
    {
        MySqlConnection GetConnection();
    }
}
