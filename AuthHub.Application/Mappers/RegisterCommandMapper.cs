using AuthHub.Application.Services.v1.Commands;
using AuthHub.Domain.Entities;
using AutoMapper;

namespace AuthHub.Application.Mappers
{
    internal class RegisterCommandMapper : Profile
    {
        public RegisterCommandMapper()
        {

            // Mapeamos: primero seleccionamos el atributo del User y luego de donde obtendremos el valor, del atributo del RegisterCommand

            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x =>  x.State))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role)); 
        }


    }
}
