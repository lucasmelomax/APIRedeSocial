
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.User.Commands.CreateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDTO>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public UpdateUserCommandHandler(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<UserResponseDTO> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            if (request.id <= 0 || request.UsersDTO is null)
                throw new InvalidOperationException("Dados do usuário inválidos.");

            var user = await _uof.UserRepository.GetById(request.id, ct);

            if (user is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            _mapper.Map(request.UsersDTO, user);

            await _uof.UserRepository.Update(request.id, user, ct);
            await _uof.Commit();

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
