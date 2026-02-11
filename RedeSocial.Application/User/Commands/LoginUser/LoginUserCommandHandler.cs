
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Account;

namespace RedeSocial.Application.User.Commands.LoginUser
{
    public class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, UserTokenDTO>
    {
        private readonly IAuthentication _authService;

        public LoginUserCommandHandler(IAuthentication authService)
        {
            _authService = authService;
        }

        public async Task<UserTokenDTO> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.LoginDTO.Email;
            var password = request.LoginDTO.Password;

            var exist = await _authService.UserExists(email);
            if (!exist)
                throw new UnauthorizedAccessException("Usuário não existe.");

            var authenticated = await _authService.AuthenticationAsync(email, password);
            if (!authenticated)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            var user = await _authService.GetUserByEmail(email);
            if (user is null)
                throw new UnauthorizedAccessException("Erro ao autenticar.");

            var token = _authService.GenerateToken(user.UsersId, user.Email);

            return new UserTokenDTO
            {
                Token = token
            };
        }
    }
}
