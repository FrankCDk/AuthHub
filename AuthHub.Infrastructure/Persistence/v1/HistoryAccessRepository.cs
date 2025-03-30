using AuthHub.Application.Interfaces;
using AuthHub.Domain.Entities;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Text;

namespace AuthHub.Infrastructure.Persistence.v1
{
    public class HistoryAccessRepository : IHistoryAccessRepository
    {

        public async Task Register(DbConnection dbConnection, string dbType, HistoryAccess historyAccess)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            StringBuilder query = new StringBuilder();
            
            switch (dbType)
            {

                case "DatabaseAcademy":
                    query.Append("INSERT INTO historial_accesos (id_usuario, fecha_acceso, ip_usuario, exito, mensaje) " +
                        " VALUES (@usuario, @fecha, @ip, @exito, @mensaje);");

                    parameters.Add(new MySqlParameter("usuario", historyAccess.IdUser));
                    parameters.Add(new MySqlParameter("fecha", historyAccess.FechaAcceso));
                    parameters.Add(new MySqlParameter("ip", historyAccess.IpUser));
                    parameters.Add(new MySqlParameter("exito", historyAccess.Exito));
                    parameters.Add(new MySqlParameter("mensaje", historyAccess.Mensaje));
                    break;

                default:
                    throw new Exception("Base de datos no implementada.");
            }

            using var cmd = dbConnection.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.AddRange(parameters.ToArray());
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
