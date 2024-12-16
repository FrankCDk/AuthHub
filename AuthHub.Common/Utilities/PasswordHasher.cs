using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthHub.Common.Utilities
{
    public static class PasswordHasher
    {
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        // Hashes a password
        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }


        // Verifies a hashed password
        public static PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}
