using AuthHub.Application.Dto.Register;
using AuthHub.Application.Interfaces;
using AuthHub.Domain.Entities;
using AuthHub.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;

namespace AuthHub.Application.Services
{
    public class RegisterService : IRegisterService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<object> _passwordHasher;

        public RegisterService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<object>();
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Mapeamos el request a un objeto de dominio
                User user = _mapper.Map<User>(request);
                user.PasswordHash = HashPassword(request.Password);
                return await _userRepository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

    }
}
