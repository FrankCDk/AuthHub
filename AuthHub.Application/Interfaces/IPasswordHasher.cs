using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthHub.Application.Interfaces
{
    public interface IPasswordHasher
    {
        (string hash, string salt) HashPassword(string password); // Retorno el hash y el salt para almacenarlo en BD.
        bool VerifyPassword(string password, string storedHash, string storedSalt); // Le paso el password, storedHash y storedSalt para verificarlo
    }
}
