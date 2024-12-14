using AuthHub.Application.Dto.Login;
using AuthHub.Application.Dto.Register;
using AuthHub.Domain.Entities;
using AutoMapper;

namespace AuthHub.Application.Dto.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<LoginRequest, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));


            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        }

    }
}
