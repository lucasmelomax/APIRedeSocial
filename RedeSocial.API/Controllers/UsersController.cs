using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Extensions;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Models;

namespace RedeSocial.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> Get([FromQuery]PaginationParams pag) {

            try {
                var users = await _userService.GetAll(pag.PageNumber, pag.PageSize);
                if (users == null) {
                    return NotFound("Não tem usuarios.");
                }
                Response.AddPaginationHeader(new PaginationHeader
                    (users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
                return Ok(users);
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpGet("{id:int}", Name="ObterUser")] 
        public async Task<ActionResult<UsersDTO>> Get(int id) {
            try {
                var user = await _userService.GetById(id);
                if (user == null) {
                    return NotFound("Esse usuario nao existe.");
                }
                return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsersDTO>> Post(UsersDTO usersDTO) {
            try {
                if (usersDTO is null) {
                    return BadRequest("Dados inválidos");
                }
                var novoUser = await _userService.Create(usersDTO);
                return new CreatedAtRouteResult("ObterUser", new { id = novoUser.UsersId }, novoUser);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("{id:int}")]

        public async Task<ActionResult<UsersDTO>> Patch(int id, JsonPatchDocument<UpdateUserDTO> updateUserDTO) {
            try {
                if (updateUserDTO == null)
                    return BadRequest();

                var updated = await _userService.Update(id, updateUserDTO);

                if (updated == null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UsersDTO>> Delete(int id) {

            try {
                if (id <= 0) {
                    return BadRequest();
                }

                await _userService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }
    }
}
