using MySql.Data.MySqlClient;
using System.Data.Common;

namespace AuthHub.Infrastructure.Connections
{
    public class MySQLDatabase
    {
      
        private readonly string _connectionString;

        public MySQLDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Obtenemos la conexion a la base de datos
        /// </summary>
        /// <returns>Conexion a BD MySQL</returns>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        //public DbConnection GetConnection()
        //{
        //    return new MySqlConnection(_connectionString);
        //}


    }
}
