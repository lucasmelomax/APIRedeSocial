
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Extensions;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsPhotosController : ControllerBase {

        private readonly IPhostPhotosService _postPhotosService;

        public PostsPhotosController(IPhostPhotosService postPhotosService) {
            _postPhotosService = postPhotosService;
        }

        [HttpGet]

        public async Task<ActionResult<PagedList<PostsPhotosDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var postsPhotosPaged = await _postPhotosService.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                postsPhotosPaged.CurrentPage,
                postsPhotosPaged.PageSize,
                postsPhotosPaged.TotalCount,
                postsPhotosPaged.TotalPages
            ));

            return Ok(postsPhotosPaged);
        }

        [HttpGet("posts/{id:int}")]

        public async Task<ActionResult<PagedList<PostsPhotosDTO>>> Get(int id, [FromQuery] PagedParams pagedParams) {
            var postsPhotosPaged = await _postPhotosService.GetByPost(id, pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                postsPhotosPaged.CurrentPage,
                postsPhotosPaged.PageSize,
                postsPhotosPaged.TotalCount,
                postsPhotosPaged.TotalPages
            ));

            return Ok(postsPhotosPaged);
        }

        [HttpGet("{id:int}", Name = "ObterPostPhoto")]

        public async Task<ActionResult<PostsPhotosDTO>> Get(int id) {
            var postsPhotos = await _postPhotosService.GetById(id);
            if (postsPhotos is null) return NotFound("Esse post nao existe.");
            return Ok(postsPhotos);
        }

        [HttpPost]

        public async Task<ActionResult<PostsPhotosDTO>> Post(PostsPhotosDTO postsPhotosDTO) {
            if (postsPhotosDTO is null) return NotFound("Post invalido.");
            var postPhoto = await _postPhotosService.Create(postsPhotosDTO);
            if (postPhoto is null) return NotFound("Post invalido.");
            return new CreatedAtRouteResult("ObterPostPhoto", new { id = postPhoto.PostsPhotosId }, postPhoto);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id) {
            if(id <= 0) return NotFound("Id invalido.");
            await _postPhotosService.DeleteById(id);
            return Ok("Post de id "+ id +"  deletado.");
        }

    }
}
