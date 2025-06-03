using AuthHub.Application.Interfaces;
using AuthHub.Domain.Entities;
using AuthHub.Utils.Helpers;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Text;

namespace AuthHub.Infrastructure.Persistence.v1
{
    public class UserRepository : IUserRepository
    {

        public UserRepository()
        {
        }

        #region Obtener el usuario
        public async Task<User> GetUser(DbConnection connection, string dbType, User request)
        {
            StringBuilder query = new ();
            List<DbParameter> parameters = new List<DbParameter>();
            User user = new User();
            switch (dbType)
            {

                case "DatabaseAcademy":
                    query.Append("SELECT id_usuario, username, correo, password_hash, salt, estado, rol FROM usuarios WHERE username = @username OR correo = @email LIMIT 1;");
                    parameters.Add(new MySqlParameter("username", request.Username));
                    parameters.Add(new MySqlParameter("email", request.Email));
                    break;
                default:
                    throw new NotImplementedException();
            }

            using var cmd = connection.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.AddRange(parameters.ToArray());
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user.Id = (int) reader["id_usuario"];
                user.Username = ConvertHelper.ToNonNullString(reader["username"]);
                user.Email = ConvertHelper.ToNonNullString(reader["correo"]);
                user.PasswordHash = ConvertHelper.ToNonNullString(reader["password_hash"]);
                user.PasswordSalt = ConvertHelper.ToNonNullString(reader["salt"]);
                user.Status = ConvertHelper.ToNonNullString(reader["estado"]);

                string roleString = ConvertHelper.ToNonNullString(reader["rol"]);

                if (Enum.TryParse<UserRole>(roleString, out var role))
                {
                    user.Role = role;
                }
                else
                {
                    throw new InvalidCastException("El tipo de rol de usuario obtenido no esta configurado.");
                }

            }

            return user;

        }

        #endregion

        #region Registrar el usuario
        public async Task<bool> Register(DbConnection connection, string dbType, User usuario)
        {
            StringBuilder query = new StringBuilder();
            List<DbParameter> parameters = new List<DbParameter>();

            switch (dbType)
            {
                case "DatabaseAcademy":
                    query.Append("INSERT INTO usuarios (nombre, apellido, correo, telefono, username, password_hash, salt, estado, " +
                        " fecha_creacion, rol)" +
                        " VALUES (@nombre, @apellido, @correo, @telefono, @username, @password_hash, @salt, @estado, " +
                        "  @fecha_creacion, @rol)");
                    parameters.Add(new MySqlParameter("nombre", usuario.FirstName));
                    parameters.Add(new MySqlParameter("apellido", usuario.LastName));
                    parameters.Add(new MySqlParameter("correo", usuario.Email));
                    parameters.Add(new MySqlParameter("telefono", usuario.PhoneNumber));
                    parameters.Add(new MySqlParameter("username", usuario.Username));
                    parameters.Add(new MySqlParameter("password_hash", usuario.PasswordHash));
                    parameters.Add(new MySqlParameter("salt", usuario.PasswordSalt));
                    parameters.Add(new MySqlParameter("estado", usuario.Status));
                    //parameters.Add(new MySqlParameter("intentos_fallidos", usuario.LastName));
                    //parameters.Add(new MySqlParameter("bloqueado_hasta", usuario.LastName));
                    //parameters.Add(new MySqlParameter("ultimo_login", usuario.LastName));
                    parameters.Add(new MySqlParameter("fecha_creacion", DateTime.Now));
                    parameters.Add(new MySqlParameter("rol", usuario.Role));
                    //parameters.Add(new MySqlParameter("permisos_json", usuario.LastName));
                    break;
                default:
                    break;
            }

            //using var cn = _databaseFactory.Connect(dbType) as DbConnection;
            //await cn!.OpenAsync();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.AddRange(parameters.ToArray());
            int execute = await cmd.ExecuteNonQueryAsync();
            return execute > 0;

        }

        #endregion

    }
}
