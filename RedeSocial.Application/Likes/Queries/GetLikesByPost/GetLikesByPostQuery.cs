
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Likes.Queries.GetLikesByPost
{
    public record GetLikesByPostQuery(int id) : IRequest<IEnumerable<LikesDTO>>
    {
    }
}
