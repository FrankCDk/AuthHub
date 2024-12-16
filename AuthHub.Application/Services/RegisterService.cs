using AuthHub.Application.Dto.Register;
using AuthHub.Application.Interfaces;
using AuthHub.Common.Utilities;
using AuthHub.Domain.Entities;
using AuthHub.Domain.Interfaces;
using AutoMapper;

namespace AuthHub.Application.Services
{
    public class RegisterService : IRegisterService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        

        public RegisterService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;            
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Mapeamos el request a un objeto de dominio
                User user = _mapper.Map<User>(request);
                user.PasswordHash = PasswordHasher.HashPassword(request.Password);
                return await _userRepository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }        

    }
}
