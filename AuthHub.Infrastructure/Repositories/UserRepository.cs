using AuthHub.Domain.Entities;
using AuthHub.Domain.Interfaces;
using AuthHub.Infrastructure.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace AuthHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IDatabaseConnection _databaseConnection;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }


        public async Task<bool> Register(User user)
        {

            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO users (Username, Email, PasswordHash, Role, CreatedAt) VALUES (@Username, @Email, @PasswordHash, @Role, @CreatedAt)");
                using var cn = _databaseConnection.GetConnection();
                await cn.OpenAsync();

                using (MySqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

           

        }
    }
}
