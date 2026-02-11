
using MediatR;
using RedeSocial.Application.DTOs;


namespace RedeSocial.Application.User.Queries.GetUserActiveQuery
{
    public record GetUserActiveQuery(string active) : IRequest<IEnumerable<UserResponseDTO>>
    {

    }
}
