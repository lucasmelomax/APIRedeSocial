
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Queries.GetPostsByUser
{
    public record GetPostsByUserQuery(int id) : IRequest<IEnumerable<PostsResponseDTO>>
    {
    }
}
