
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Likes.Queries.GetLikesByPost
{
    public class GetLikesByPostQueryHandler : IRequestHandler<GetLikesByPostQuery, IEnumerable<LikesDTO>>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public GetLikesByPostQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LikesDTO>> Handle(GetLikesByPostQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.LikesRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(c => c.PostsId == request.id).ToList();

            return _mapper.Map<IEnumerable<LikesDTO>>(query);

        }
    }
}
