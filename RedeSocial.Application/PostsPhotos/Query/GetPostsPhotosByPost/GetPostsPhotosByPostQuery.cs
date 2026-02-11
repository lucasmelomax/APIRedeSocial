
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.PostsPhotos.Query.GetPostsPhotosByPost
{
    public record GetPostsPhotosByPostQuery(int id) : IRequest<IEnumerable<PostsPhotosDTO>>
    {
    }
}
