
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Commands.UpdatePosts
{
    public record UpdatePostsCommand(int id, PostsDTO postsDTO) : IRequest<PostsResponseDTO>
    {
    }
}
