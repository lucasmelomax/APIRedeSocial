
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Commands.CreateUser
{
    public record CreateUserCommand(UsersDTO UsersDTO)
        : IRequest<UserTokenDTO>;

}
