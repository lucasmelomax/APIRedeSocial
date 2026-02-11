
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.User.Commands.CreateUser
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, UserTokenDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IAuthentication _authService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IUnitOfWork uof,
            IAuthentication authService,
            IMapper mapper)
        {
            _uof = uof;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<UserTokenDTO> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var emailExists = await _authService.UserExists(request.UsersDTO.Email);

            if (emailExists) throw new InvalidOperationException("Este email já possui cadastro.");

            var user = _mapper.Map<Users>(request.UsersDTO);

            await _uof.UserRepository.Create(user, ct);
            await _uof.Commit();

            var token = _authService.GenerateToken(user.UsersId, user.Email);

            return new UserTokenDTO
            {
                Token = token
            };
        }
    }
}
