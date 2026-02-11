using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.PostsPhotos.Query.GetPostsPhotos
{
    public record GetPostsPhotosQuery() : IRequest<IEnumerable<PostsPhotosDTO>>
    {

    }
}
