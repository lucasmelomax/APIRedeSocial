

using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs.Comments;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Comments.Queries.GetCommentsById
{
    public class GetCommentsByIdQueryHandler : IRequestHandler<GetCommentsByIdQuery, CommentsDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetCommentsByIdQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<CommentsDTO> Handle(GetCommentsByIdQuery request, CancellationToken ct)
        {
            if (request.id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var comment = await _uof.CommentsRepository.GetById(request.id, ct);
            var commentDTO = _mapper.Map<CommentsDTO>(comment);
            return commentDTO;
        }
    }
}
