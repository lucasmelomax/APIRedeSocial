
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Commands.LoginUser
{
    public record LoginUserCommand(LoginDTO LoginDTO) : IRequest<UserTokenDTO>;
}
