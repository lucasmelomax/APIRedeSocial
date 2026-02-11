
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Queries.GetPosts
{
    public record GetPostsQuery : IRequest<IEnumerable<PostsResponseDTO>>
    {
    }
}
