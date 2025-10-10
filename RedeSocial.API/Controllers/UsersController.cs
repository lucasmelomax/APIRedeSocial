
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Extensions;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly IUserService _userService;
        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<UserResponseDTO>>> Get([FromQuery] PagedParams pagedParams) {
            var usersPaged = await _userService.GetAll(pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                usersPaged.CurrentPage,
                usersPaged.PageSize,
                usersPaged.TotalCount,
                usersPaged.TotalPages
            ));

            return Ok(usersPaged);
        }

        [HttpGet("active/{active:alpha}")]
        public async Task<ActionResult<PagedList<UserResponseDTO>>> Get(string active, [FromQuery] PagedParams pagedParams) {
            var usersPaged = await _userService.GetActiveUsers(active, pagedParams);

            Response.AddPaginationHeader(new PaginationHeader(
                usersPaged.CurrentPage,
                usersPaged.PageSize,
                usersPaged.TotalCount,
                usersPaged.TotalPages
            ));

            return Ok(usersPaged);
        }


        [HttpGet("{id:int}", Name = "ObterUser")]
        public async Task<ActionResult<UserResponseDTO>> Get(int id) {

            var user = await _userService.GetById(id);
            if (user == null) {
                return NotFound("Esse usuario nao existe.");
            }

            return Ok(user);

        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> Post(UsersDTO usersDTO) {

            if (usersDTO is null) {
                return BadRequest("Dados inválidos");
            }

            var novoUser = await _userService.Create(usersDTO);

            return new CreatedAtRouteResult("ObterUser", new { id = novoUser.UsersId }, novoUser);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponseDTO>> Put(int id, [FromBody] UserPutDTO usersDTO) {

            if (usersDTO is null) {
                return BadRequest("Dados inválidos");
            }
            var novoUser = await _userService.Put(id, usersDTO);
            return new CreatedAtRouteResult("ObterUser", new { id = novoUser.UsersId }, novoUser);


        }

        [HttpPatch("{id:int}")]

        public async Task<ActionResult<UserResponseDTO>> Patch(int id, JsonPatchDocument<UsersDTO> UserDTO) {

            if (UserDTO == null)
                return BadRequest();

            var updated = await _userService.Patch(id, UserDTO);

            if (updated == null)
                return NotFound();

            return Ok(updated);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserResponseDTO>> Delete(int id) {

            if (id <= 0) {
                return BadRequest("O id precisa ser maior que 0.");
            }

            await _userService.DeleteById(id);
            return Ok("Usuario de id = " + id + " excluido!");

        }
    }
}
