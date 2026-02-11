using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Queries.GetPostsById
{
    public record GetPostsByIdQuery(int id) : IRequest<PostsResponseDTO>
    {
    }
}
