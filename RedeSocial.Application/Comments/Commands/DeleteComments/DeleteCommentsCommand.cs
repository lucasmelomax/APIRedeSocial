
using MediatR;

namespace RedeSocial.Application.Comments.Commands.DeleteComments
{
    public record DeleteCommentsCommand(int id) : IRequest<Unit>
    {
    }
}
