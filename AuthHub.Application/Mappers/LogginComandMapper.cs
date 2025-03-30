using AuthHub.Application.Auth.v1.Commands;
using AuthHub.Application.Dtos.Response;
using AuthHub.Domain.Entities;
using AutoMapper;

namespace AuthHub.Application.Mappers
{
    public class LogginComandMapper : Profile
    {
        public LogginComandMapper() 
        {

            CreateMap<LoginCommand, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));


            CreateMap<User, LoginDto>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Username));
                

        }
    }
}    