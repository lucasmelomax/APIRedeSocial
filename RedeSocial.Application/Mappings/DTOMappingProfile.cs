
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.DTOs.Comments;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Mappings
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {

            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Users, UserResponseDTO>().ReverseMap();
            CreateMap<Domain.Models.Posts, PostsDTO>().ReverseMap();
            CreateMap<Domain.Models.Posts, PostsResponseDTO>().ReverseMap();
            CreateMap<Domain.Models.PostsPhotos, PostsPhotosDTO>().ReverseMap();
            CreateMap<Domain.Models.Comments, CommentsDTO>().ReverseMap();
            CreateMap<Domain.Models.Comments, CreateCommentsDTO>().ReverseMap();
            CreateMap<Domain.Models.Likes, LikesDTO>().ReverseMap();

        }
    }
}
