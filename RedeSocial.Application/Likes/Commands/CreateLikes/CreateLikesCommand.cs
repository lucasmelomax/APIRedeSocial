
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Likes.Commands.CreateLikes
{
    public record CreateLikesCommand(LikesDTO likesDTO) : IRequest<LikesDTO>
    {
    }
}
