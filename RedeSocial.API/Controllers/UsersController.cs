
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.User.Commands.CreateUser;
using RedeSocial.Application.User.Commands.DeleteUser;
using RedeSocial.Application.User.Commands.PatchUser;
using RedeSocial.Application.User.Queries.GetUser;
using RedeSocial.Application.User.Queries.GetUserActiveQuery;
using RedeSocial.Application.User.Queries.GetUserById;
using RedeSocial.Application.User.Queries.GetUserByUsername;


namespace RedeSocial.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll(CancellationToken ct)
        {
            var users = await _mediator.Send(new GetUserQuery(), ct);

            return Ok(users);
        }

        [HttpGet("active/{active:alpha}")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsersActive(string active, CancellationToken ct)
        {
            var users = await _mediator.Send(new GetUserActiveQuery(active), ct);

            return Ok(users);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsersByUsername(string username, CancellationToken ct)
        {
            var users = await _mediator.Send(new GetUserByUsernameQuery(username), ct);

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDTO>> GetById([FromRoute] GetUserByIdQuery query, CancellationToken ct)
        {

            var user = await _mediator.Send(query, ct);

            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponseDTO>> Put([FromBody] UpdateUserCommand command, CancellationToken ct)
        {

            var result = await _mediator.Send(command, ct);

            return Ok(result);

        }

        [HttpPatch("{id:int}")]

        public async Task<ActionResult<UserResponseDTO>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<UsersDTO> userDTO, CancellationToken ct)
        {

            var updated = await _mediator.Send(new PatchUserCommand(id, userDTO), ct);

            return Ok(updated);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteUserCommand command, CancellationToken ct)
        {

            await _mediator.Send(command, ct);

            return NoContent();

        }
    }
}
