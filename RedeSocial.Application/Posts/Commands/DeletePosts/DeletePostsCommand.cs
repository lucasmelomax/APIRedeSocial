
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Commands.DeletePosts
{
    public record DeletePostsCommand(int id) : IRequest<Unit>
    {
    }
}
