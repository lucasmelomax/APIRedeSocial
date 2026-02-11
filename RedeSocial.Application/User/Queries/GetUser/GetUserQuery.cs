
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Queries.GetUser
{
    public record GetUserQuery() : IRequest<IEnumerable<UserResponseDTO>>
    {

    }
}
