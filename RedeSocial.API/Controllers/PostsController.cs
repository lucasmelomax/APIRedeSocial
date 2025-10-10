using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RedeSocial.API.Extensions;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Application.Services;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase {

        private readonly IPostsService _service;
        public PostsController(IPostsService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<PostsResponseDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var postsPaged = await _service.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                postsPaged.CurrentPage,
                postsPaged.PageSize,
                postsPaged.TotalCount,
                postsPaged.TotalPages
            ));

            return Ok(postsPaged);
        }

        [HttpGet("PostsByUser")]
        public async Task<ActionResult<PagedList<PostsResponseDTO>>> Get( int id, [FromQuery] PagedParams pagedParams) {
            var postsPaged = await _service.GetAllPostsByUser(id, pagedParams);

            if (postsPaged.Count() == 0) {
                return NotFound("Esse usuario nao tem nenhum post.");
            }

            Response.AddPaginationHeader(new PaginationHeader(
                postsPaged.CurrentPage,
                postsPaged.PageSize,
                postsPaged.TotalCount,
                postsPaged.TotalPages
            ));

            return Ok(postsPaged);
        }

        [HttpGet("{id:int}", Name = "ObterPost")]
        public async Task<ActionResult<PostsResponseDTO>> Get(int id) {
            var post = await _service.GetById(id);

            if (post == null) {
                return NotFound("Esse post nao existe.");
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostsResponseDTO>> Post(PostsDTO postsDTO) {
            if (postsDTO == null) {
                return NotFound("Esse post nao existe.");
            }

            var post = await _service.Create(postsDTO);

            return new CreatedAtRouteResult("ObterPost", new { id = post.UsersId }, post);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PostsResponseDTO>> Put(int id, [FromBody] PostsDTO postsDTO) {
            if (postsDTO == null) {
                return NotFound("Esse post nao existe.");
            }

            var post = await _service.Put(id, postsDTO);

            return new CreatedAtRouteResult("ObterPost", new { id = post.UsersId }, post);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            await _service.Delete(id);

            return Ok("Usuario de id = " +id+ " excluido!");
        }

    }
}
