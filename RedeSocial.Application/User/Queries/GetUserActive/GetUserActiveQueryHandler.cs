
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.User.Queries.GetUserActiveQuery
{
    public class GetUserActiveQueryHandler : IRequestHandler<GetUserActiveQuery, IEnumerable<UserResponseDTO>>
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetUserActiveQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDTO>> Handle(
        GetUserActiveQuery request,
        CancellationToken ct)
        {
            var query = _uof.UserRepository.GetAll();

            query = request.active.ToLower() switch
            {
                "active" => query.Where(u => u.Active),
                "inative" => query.Where(u => !u.Active),
                _ => throw new ArgumentException(
                    "Os valores devem ser 'active' ou 'inative'.")
            };

            var users = query
                .OrderBy(u => u.UsersId)
                .ToList();

            if (!users.Any())
                throw new KeyNotFoundException("Nenhum usuário encontrado.");

            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

    }
}
