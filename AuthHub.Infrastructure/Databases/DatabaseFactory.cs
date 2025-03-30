using AuthHub.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace AuthHub.Infrastructure.Databases
{
    public class DatabaseFactory : IDatabaseConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DatabaseFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connect(string dbType)
        {

            var connectionString = dbType switch
            {
                "DatabaseAcademy" => _configuration.GetConnectionString("AuthAcademy"),
                "SQLServer" => _configuration.GetConnectionString("SQLServer"),
                _ => throw new NotImplementedException($"Database type {dbType} not supported.")
            };


            return dbType switch
            {
                "DatabaseAcademy" => new MySqlConnection(connectionString),
                "SQLServer" => new MySqlConnection(connectionString),
                _ => throw new NotImplementedException($"Database type {dbType} not supported.")
            };
        }
    }
}
