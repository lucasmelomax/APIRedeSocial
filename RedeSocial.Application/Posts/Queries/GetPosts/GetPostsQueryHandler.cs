
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostsResponseDTO>>
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostsResponseDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.PostsRepository.GetAll()
            .OrderBy(p => p.PostsId).ToList();

            return _mapper.Map<IEnumerable<PostsResponseDTO>>(query);
        }
    }
}
