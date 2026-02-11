
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.PostsPhotos.Command.DeletePostPhotos
{
    public record DeletePostsPhotosCommand(int id) : IRequest<Unit>
    {
    }
}
