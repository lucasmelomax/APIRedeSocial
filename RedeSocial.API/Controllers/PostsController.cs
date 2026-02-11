
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Posts.Commands.CreatePosts;
using RedeSocial.Application.Posts.Commands.DeletePosts;
using RedeSocial.Application.Posts.Commands.UpdatePosts;
using RedeSocial.Application.Posts.Queries.GetPosts;
using RedeSocial.Application.Posts.Queries.GetPostsById;
using RedeSocial.Application.Posts.Queries.GetPostsByUser;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostsResponseDTO>>> GetAll(CancellationToken ct)
        {
            var posts = await _mediator.Send(new GetPostsQuery(), ct);

            return Ok(posts);
        }

        [HttpGet("PostsByUser/{id:int}")]
        public async Task<ActionResult<IEnumerable<PostsResponseDTO>>> GetPostsByUser(int id, CancellationToken ct)
        {
            var posts = await _mediator.Send(new GetPostsByUserQuery(id), ct);

            return Ok(posts);
        }

        [HttpGet("{id:int}", Name = "ObterPost")]
        public async Task<ActionResult<PostsResponseDTO>> GetById(int id, CancellationToken ct)
        {

            var post = await _mediator.Send(new GetPostsByIdQuery(id), ct);

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostsResponseDTO>> Post(CreatePostsCommand command, CancellationToken ct)
        {

            var post = await _mediator.Send(command, ct);

            return new CreatedAtRouteResult("ObterPost", new { id = post.PostsId }, post);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PostsResponseDTO>> Put(int id, PostsDTO postsDTO, CancellationToken ct)
        {

            var post = await _mediator.Send(new UpdatePostsCommand(id, postsDTO), ct);

            return Ok(post);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeletePostsCommand command, CancellationToken ct)
        {

            await _mediator.Send(command, ct);

            return NoContent();
        }

    }
}
