
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.User.Commands.CreateUser;
using RedeSocial.Application.User.Commands.LoginUser;
using RedeSocial.Domain.Account;

namespace RedeSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTokenController : ControllerBase
    {

        private readonly IAuthentication _service;
        private readonly IMediator _mediator;

        public UserTokenController(IAuthentication service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserTokenDTO>> Incluir(UsersDTO usersDTO, CancellationToken ct)
        {

            var result = await _mediator.Send(new CreateUserCommand(usersDTO), ct);

            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserTokenDTO>> Selecionar(LoginDTO loginDTO, CancellationToken ct)
        {

            if (loginDTO is null)
                return BadRequest("Dados inválidos.");

            var token = await _mediator.Send(
                new LoginUserCommand(loginDTO), ct);

            return Ok(token);

        }

    }
}
