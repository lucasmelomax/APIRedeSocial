

using MediatR;

namespace RedeSocial.Application.User.Commands.DeleteUser
{
    public record DeleteUserCommand(int id) : IRequest<Unit>
    {
    }
}
