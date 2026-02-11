
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Commands.PatchUser
{
    public record PatchUserCommand(int id, JsonPatchDocument<UsersDTO> userDTO) : IRequest<UserResponseDTO>
    {
    }
}
