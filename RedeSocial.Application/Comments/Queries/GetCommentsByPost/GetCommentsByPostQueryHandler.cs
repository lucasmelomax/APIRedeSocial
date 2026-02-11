

using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs.Comments;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Comments.Queries.GetCommentsByPost
{
    public class GetCommentsByPostQueryHandler : IRequestHandler<GetCommentsByPostQuery, IEnumerable<CommentsDTO>>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetCommentsByPostQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentsDTO>> Handle(GetCommentsByPostQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.CommentsRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(c => c.PostsId == request.id);
            
            return _mapper.Map<IEnumerable<CommentsDTO>>(query);
        }
    }
}
