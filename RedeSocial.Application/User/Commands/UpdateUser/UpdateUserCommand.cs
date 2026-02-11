
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Commands.CreateUser
{
    public record UpdateUserCommand(int id, UsersDTO UsersDTO) : IRequest<UserResponseDTO>
    {
    }
}
