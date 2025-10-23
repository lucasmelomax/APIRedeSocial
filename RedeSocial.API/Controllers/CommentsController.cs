using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Extensions;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase {

        private readonly ICommentsService _service;

        public CommentsController(ICommentsService commentsService) {
            _service = commentsService;
        }

        [HttpGet]

        public async Task<ActionResult<PagedList<CommentsDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var commentPaged = await _service.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                commentPaged.CurrentPage,
                commentPaged.PageSize,
                commentPaged.TotalCount,
                commentPaged.TotalPages
            ));

            return Ok(commentPaged);
        }

        [HttpGet("CommentsByPost")]
        public async Task<ActionResult<PagedList<CommentsDTO>>> Get(int id, [FromQuery] PagedParams pagedParams) {
            var commentPaged = await _service.GetAllByPost(id, pagedParams);

            if (commentPaged.Count() == 0) {
                return NotFound("Esse post nao tem nenhum comentario.");
            }

            Response.AddPaginationHeader(new PaginationHeader(
                commentPaged.CurrentPage,
                commentPaged.PageSize,
                commentPaged.TotalCount,
                commentPaged.TotalPages
            ));

            return Ok(commentPaged);
        }

        [HttpGet("{id:int}", Name = "ObterComment")]
        public async Task<ActionResult<CommentsDTO>> Get(int id) {
            var post = await _service.GetById(id);

            if (post == null) {
                return NotFound("Esse cometario nao existe.");
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<CommentsDTO>> Post(CommentsDTO commentsDTO) {
            if (commentsDTO == null) {
                return NotFound("Esse comentario nao é valido.");
            }

            var comments = await _service.Create(commentsDTO);

            return new CreatedAtRouteResult("ObterComment", new { id = comments.UsersId }, comments);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            await _service.Delete(id);

            return Ok("Comment de id = " + id + " excluido!");
        }
    }
}
