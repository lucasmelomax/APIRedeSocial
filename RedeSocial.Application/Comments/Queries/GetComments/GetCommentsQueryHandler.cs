

using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs.Comments;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Comments.Queries.GetComments
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<CommentsDTO>>
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentsDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.CommentsRepository.GetAll()
            .OrderBy(p => p.UsersId);

            return _mapper.Map<IEnumerable<CommentsDTO>>(query);
        }
    }
}
