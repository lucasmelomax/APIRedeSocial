
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappings
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile() { 
        
            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Users, UserResponseDTO>().ReverseMap();
            CreateMap<Users, UserPutDTO>().ReverseMap();

        }
    }
}
