using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.User.Queries.GetUserByUsername
{
    public record GetUserByUsernameQuery(string Username) : IRequest<IEnumerable<UserResponseDTO>>
    {

    }
}
