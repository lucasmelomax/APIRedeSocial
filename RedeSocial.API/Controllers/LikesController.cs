
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Likes.Commands.CreateLikes;
using RedeSocial.Application.Likes.Commands.DeleteLikes;
using RedeSocial.Application.Likes.Queries.GetLikes;
using RedeSocial.Application.Likes.Queries.GetLikesById;
using RedeSocial.Application.Likes.Queries.GetLikesByPost;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LikesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikesDTO>>> GetAll(CancellationToken ct)
        {
            var likes = await _mediator.Send(new GetLikesQuery(), ct);

            return Ok(likes);
        }

        [HttpGet("LikesByPost/{id:int}")]
        public async Task<ActionResult<IEnumerable<LikesDTO>>> GetAllByPost(int id, CancellationToken ct)
        {
            var likes = await _mediator.Send(new GetLikesByPostQuery(id), ct);

            return Ok(likes);
        }

        [HttpGet("{id:int}", Name = "ObterLikes")]
        public async Task<ActionResult<LikesDTO>> GetById(int id, CancellationToken ct)
        {
            var likes = await _mediator.Send(new GetLikesByIdQuery(id), ct);

            return Ok(likes);
        }

        [HttpPost]
        public async Task<ActionResult<LikesDTO>> Post(LikesDTO likesDTO, CancellationToken ct)
        {
            var likes = await _mediator.Send(new CreateLikesCommand(likesDTO), ct);

            return new CreatedAtRouteResult("ObterLikes", new { id = likes.UsersId }, likes);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteLikesCommand(id), ct);

            return NoContent();
        }
    }
}
