using AuthHub.Application.Interfaces;
using System.Security.Cryptography;

namespace AuthHub.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {

        /// <summary>
        /// Hasheamos el password asignandole un salt aleatorio para que la contraseña no se vea como la misma en nuestra base de datos
        /// </summary>
        /// <param name="password">Contraseña que usara el usuario</param>
        /// <returns></returns>
        public (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hashbytes = pbkdf2.GetBytes(32);
                return (Convert.ToBase64String(hashbytes), Convert.ToBase64String(saltBytes));
            }

        }

        /// <summary>
        /// Aca usamos la contraseña enviada por el cliente
        /// </summary>
        /// <param name="password">Contraseña enviada por el usuario</param>
        /// <param name="storedHash">Contraseña hasheada almacenada en base de datos</param>
        /// <param name="storedSalt">Salt almacenado en base de datos.</param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hashBytes) == storedHash;
            }

        }
    }
}
