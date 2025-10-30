using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.API.Models;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Application.Services;
using RedeSocial.Domain.Account;

namespace RedeSocial.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserTokenController : ControllerBase {

        private readonly IAuthentication _service;
        private readonly IUserService _userService;

        public UserTokenController(IAuthentication service, IUserService userService) {
            _service = service;
            _userService = userService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserToken>> Incluir(UsersDTO usersDTO) {
            if (usersDTO == null) return BadRequest("Dados invalidos.");

            var emailExist = await _service.UserExists(usersDTO.Email);

            if (emailExist) {
                return BadRequest("Este email ja possui um cadastro.");
            }

            var usuario = await _userService.Create(usersDTO);

            if (usuario == null) return BadRequest("Ocorreu um erro ao cadastrar.");

            var token = _service.GenerateToken(usuario.UsersId, usuario.Email);

            return new UserToken {
                Token = token,
            };
        }


        [HttpPost("login")]

        public async Task<ActionResult<UserToken>> Selecionar(LoginModel loginModel) {
            var existe = await _service.UserExists(loginModel.Email);
            if (!existe) {
                return Unauthorized("Usuario nao existe.");
            }

            var result = await _service.AuthenticationAsync(loginModel.Email, loginModel.Password);
            if (!result) {
                return Unauthorized("Usuario ou senha invalidos.");
            }

            var usuario = await _service.GetUserByEmail(loginModel.Email);

            var token = _service.GenerateToken(usuario.UsersId, usuario.Email);

            return new UserToken {
                Token = token,
            };

        }

    }
}
