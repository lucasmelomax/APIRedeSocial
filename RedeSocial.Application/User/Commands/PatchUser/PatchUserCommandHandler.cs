
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.User.Commands.PatchUser
{
    internal class PatchUserCommandHandler : IRequestHandler<PatchUserCommand, UserResponseDTO>
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public PatchUserCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<UserResponseDTO> Handle(PatchUserCommand request, CancellationToken ct)
        {

            if (request.userDTO is null || request.id <= 0)
                throw new InvalidOperationException("Dados do usuario invalidos.");

            var user = await _uof.UserRepository.GetById(request.id, ct);

            if (user is null)
                throw new InvalidOperationException("Usuario não encontrado.");

            var dto = _mapper.Map<UsersDTO>(user);

            request.userDTO.ApplyTo(dto);

            _mapper.Map(dto, user);

            var updated = await _uof.UserRepository.Update(request.id, user, ct);
            await _uof.Commit();

            return _mapper.Map<UserResponseDTO>(updated);
        }
    }
}
