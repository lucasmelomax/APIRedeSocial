
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;


namespace RedeSocial.Application.User.Queries.GetUserByUsername
{
    public class GetUsersByUsernameQueryHandler
    : IRequestHandler<GetUserByUsernameQuery, IEnumerable<UserResponseDTO>>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetUsersByUsernameQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDTO>> Handle(
        GetUserByUsernameQuery request,
        CancellationToken ct)
        {
            var query = _uof.UserRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                query = query
                    .Where(u => u.Username.Contains(request.Username))
                    .OrderBy(u => !u.Username.StartsWith(request.Username))
                    .ThenBy(u => u.Username);
            }

            var users = query.ToList();

            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

    }
}
