
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.PostsPhotos.Command.CreatePostsPhotos;
using RedeSocial.Application.PostsPhotos.Command.DeletePostPhotos;
using RedeSocial.Application.PostsPhotos.Query.GetPostsPhotos;
using RedeSocial.Application.PostsPhotos.Query.GetPostsPhotosById;
using RedeSocial.Application.PostsPhotos.Query.GetPostsPhotosByPost;


namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsPhotosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsPhotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<PostsPhotosDTO>>> GetAll(CancellationToken ct)
        {
            var postsPhotos = await _mediator.Send(new GetPostsPhotosQuery(), ct);
            return Ok(postsPhotos);
        }

        [HttpGet("posts/{id:int}")]

        public async Task<ActionResult<IEnumerable<PostsPhotosDTO>>> GetByPost(int id, CancellationToken ct)
        {
            var posts = await _mediator.Send(new GetPostsPhotosByPostQuery(id), ct);

            return Ok(posts);
        }

        [HttpGet("{id:int}", Name = "ObterPostPhoto")]

        public async Task<ActionResult<PostsPhotosDTO>> GetById(int id, CancellationToken ct)
        {
            var postsPhotos = await _mediator.Send(new GetPostsPhotosByIdQuery(id), ct);

            return Ok(postsPhotos);
        }

        [HttpPost]
        public async Task<ActionResult<PostsPhotosDTO>> Post(PostsPhotosDTO postsPhotosDTO, CancellationToken ct)
        {

            var postPhoto = await _mediator.Send(new CreatePostsPhotosCommand(postsPhotosDTO), ct);

            return new CreatedAtRouteResult("ObterPostPhoto", new { id = postPhoto.PostsPhotosId }, postPhoto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeletePostsPhotosCommand(id), ct);

            return NoContent();
        }
    }
}
