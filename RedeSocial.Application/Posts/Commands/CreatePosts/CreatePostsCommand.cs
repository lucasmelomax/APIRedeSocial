
using MediatR;
using RedeSocial.Application.DTOs;

namespace RedeSocial.Application.Posts.Commands.CreatePosts
{
    public record CreatePostsCommand(PostsDTO postsDTO) :IRequest<PostsResponseDTO>
    {
    }
}
