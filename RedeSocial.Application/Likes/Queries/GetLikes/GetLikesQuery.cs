using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Likes.Queries.GetLikes
{
    public record GetLikesQuery() : IRequest<IEnumerable<LikesDTO>>
    {
    }
}
