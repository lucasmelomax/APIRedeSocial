using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.User.Queries.GetUser
{
    public class GetUserQueryHandler
        : IRequestHandler<GetUserQuery, IEnumerable<UserResponseDTO>>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDTO>> Handle(
            GetUserQuery request,
            CancellationToken ct)
        {
            var users = _uof.UserRepository
                .GetAll()
                .OrderBy(u => u.UsersId)
                .ToList();

            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }
    }
}
