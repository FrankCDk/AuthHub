
using AuthHub.Domain.Interfaces;
using AuthHub.Infrastructure.Interfaces;
using MySql.Data.MySqlClient;

namespace AuthHub.Infrastructure.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {

        private readonly IDatabaseConnection _databaseConnection;

        public UserAuthenticationRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<bool> VerifyCredentialsAsync(string username, string password)
        {

            try
            {
                using var cn = _databaseConnection.GetConnection();
                await cn.OpenAsync();

                using (MySqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                    cmd.Parameters.Add(new MySqlParameter("@username", username));
                    cmd.Parameters.Add(new MySqlParameter("@password", password));

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
