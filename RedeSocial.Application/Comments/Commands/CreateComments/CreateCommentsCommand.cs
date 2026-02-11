

using MediatR;
using RedeSocial.Application.DTOs.Comments;

namespace RedeSocial.Application.Comments.Commands.CreateComments
{
    public record CreateCommentsCommand(CreateCommentsDTO commentsDTO) : IRequest<CommentsDTO>
    {
    }
}
