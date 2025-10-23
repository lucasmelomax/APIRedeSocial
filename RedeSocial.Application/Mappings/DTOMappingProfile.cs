
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
            CreateMap<Posts, PostsDTO>().ReverseMap();
            CreateMap<Posts, PostsResponseDTO>().ReverseMap();
            CreateMap<PostsPhotos, PostsPhotosDTO>().ReverseMap();
            CreateMap<Comments, CommentsDTO>().ReverseMap();
            CreateMap<Likes, LikesDTO>().ReverseMap();
            CreateMap<Followers, FollowersDTO>().ReverseMap();

        }
    }
}
