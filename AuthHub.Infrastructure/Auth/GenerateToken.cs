using AuthHub.Application.Interfaces;
using AuthHub.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthHub.Infrastructure.Auth
{
    public class GenerateToken(IConfiguration configuration) : IGenerateToken
    {

        private readonly IConfiguration _configuration = configuration;

        public string GenerateJWT(User usuario)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email), //Identificador unico del usuario
                new Claim(JwtRegisteredClaimNames.Name, usuario.Username),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, "AuthHub"),
                new Claim("Role", usuario.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // URL del servicio de autenticacion
                audience: _configuration["Jwt:Audience"], // URL del servicio de funcionalidades
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
