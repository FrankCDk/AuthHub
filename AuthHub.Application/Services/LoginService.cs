using AuthHub.Application.Dto.Login;
using AuthHub.Application.Interfaces;
using AuthHub.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using AuthHub.Application.Models;


namespace AuthHub.Application.Services
{
    public class LoginService : ILoginService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Result<LoginResponse>> Login(LoginRequest request)
        {

            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return Result.Failure<LoginResponse>("El nombre de usuario y la contraseña son requeridos.");
            }

            LoginResponse response = new LoginResponse();

            //Nos conectamos a la base de datos
            bool result = await _userRepository.VerifyCredentialsAsync(request.Username, request.Password);

            if (!result)
            {
                throw new Exception("Credenciales incorrectas.");
            }

            response.Username = request.Username;
            response.Token = GenerateJwtToken(request.Username);

            //Devolvemos la respuesta
            return response;

        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
