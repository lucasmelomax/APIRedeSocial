
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.Comments.Commands.CreateComments;
using RedeSocial.Application.Comments.Commands.DeleteComments;
using RedeSocial.Application.Comments.Queries.GetComments;
using RedeSocial.Application.Comments.Queries.GetCommentsById;
using RedeSocial.Application.Comments.Queries.GetCommentsByPost;
using RedeSocial.Application.DTOs.Comments;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetAll(CancellationToken ct)
        {
            var comments = await _mediator.Send(new GetCommentsQuery(), ct);

            return Ok(comments);
        }

        [HttpGet("CommentsByPost/{id:int}")]
        public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetAllByPost(int id,CancellationToken ct)
        {

            var comments= await _mediator.Send(new GetCommentsByPostQuery(id), ct);

            return Ok(comments);
        }

        [HttpGet("{id:int}", Name = "ObterComment")]
        public async Task<ActionResult<CommentsDTO>> GetById(int id, CancellationToken ct)
        {

            var post = await _mediator.Send(new GetCommentsByIdQuery(id), ct);

            if (post == null) return NotFound("Esse cometario nao existe.");

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<CommentsDTO>> Post(CreateCommentsDTO commentsDTO, CancellationToken ct)
        {

            var comments = await _mediator.Send(new CreateCommentsCommand(commentsDTO), ct);

            if (comments == null) return BadRequest("Esse comentario nao existe.");

            return new CreatedAtRouteResult("ObterComment", new { id = comments.CommentsId }, comments);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {

            await _mediator.Send(new DeleteCommentsCommand(id), ct);

            return Ok("Comment de id = " + id + " excluido!");
        }
    }
}
