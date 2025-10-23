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
    public class LikesController : ControllerBase {


        private readonly ILikesService _service;

        public LikesController(ILikesService likesService) {
            _service = likesService;
        }

        [HttpGet]

        public async Task<ActionResult<PagedList<LikesDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var likesPaged = await _service.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                likesPaged.CurrentPage,
                likesPaged.PageSize,
                likesPaged.TotalCount,
                likesPaged.TotalPages
            ));

            return Ok(likesPaged);
        }

        [HttpGet("LikesByPost")]
        public async Task<ActionResult<PagedList<LikesDTO>>> Get(int id, [FromQuery] PagedParams pagedParams) {
            var likesPaged = await _service.GetAllByPost(id, pagedParams);

            if (likesPaged.Count() == 0) {
                return NotFound("Esse post nao tem nenhum like.");
            }

            Response.AddPaginationHeader(new PaginationHeader(
                likesPaged.CurrentPage,
                likesPaged.PageSize,
                likesPaged.TotalCount,
                likesPaged.TotalPages
            ));

            return Ok(likesPaged);
        }

        [HttpGet("{id:int}", Name = "ObterLikes")]
        public async Task<ActionResult<LikesDTO>> Get(int id) {
            var likes = await _service.GetById(id);

            if (likes == null) {
                return NotFound("Esse like nao existe.");
            }
            return Ok(likes);
        }

        [HttpPost]
        public async Task<ActionResult<LikesDTO>> Post(LikesDTO likesDTO) {
            if (likesDTO == null) {
                return NotFound("Esse like nao existe.");
            }

            var likes = await _service.Create(likesDTO);

            return new CreatedAtRouteResult("ObterLikes", new { id = likes.UsersId }, likes);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            await _service.Delete(id);

            return Ok("Like de id = " + id + " excluido!");
        }
    }
}
