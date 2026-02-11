
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Likes.Queries.GetLikesById
{
    public record GetLikesByIdQuery(int id) : IRequest<LikesDTO>
    {
    }
}
