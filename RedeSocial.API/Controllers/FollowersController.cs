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
    public class FollowersController : ControllerBase {


        private readonly IFollowersService _service;

        public FollowersController(IFollowersService followersService) {
            _service = followersService;
        }

        [HttpGet]

        public async Task<ActionResult<PagedList<FollowersDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var followersPaged = await _service.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                followersPaged.CurrentPage,
                followersPaged.PageSize,
                followersPaged.TotalCount,
                followersPaged.TotalPages
            ));

            return Ok(followersPaged);
        }

        [HttpGet("FollowersByUser")]
        public async Task<ActionResult<PagedList<FollowersDTO>>> Get(int id, [FromQuery] PagedParams pagedParams) {
            var followerPaged = await _service.GetAllByUser(id, pagedParams);

            if (followerPaged.Count() == 0) {
                return NotFound("Esse user nao tem nenhum seguidor.");
            }

            Response.AddPaginationHeader(new PaginationHeader(
                followerPaged.CurrentPage,
                followerPaged.PageSize,
                followerPaged.TotalCount,
                followerPaged.TotalPages
            ));

            return Ok(followerPaged);
        }

        [HttpGet("{id:int}", Name = "ObterFollower")]
        public async Task<ActionResult<FollowersDTO>> Get(int id) {
            var follower = await _service.GetById(id);

            if (follower == null) {
                return NotFound("Esse follower nao existe.");
            }
            return Ok(follower);
        }

        [HttpPost]
        public async Task<ActionResult<FollowersDTO>> Post(FollowersDTO followerDTO) {
            if (followerDTO == null) {
                return NotFound("Esse follower nao existe.");
            }

            var follower = await _service.Create(followerDTO);

            return new CreatedAtRouteResult("ObterFollower", new { id = follower.UsersId }, follower);
        }
    }
}
