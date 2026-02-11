
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Likes.Queries.GetLikes
{
    public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, IEnumerable<LikesDTO>>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public GetLikesQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LikesDTO>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.LikesRepository.GetAll()
            .OrderBy(p => p.UsersId).ToList();

            return _mapper.Map<IEnumerable<LikesDTO>>(query);
        }
    }
}
