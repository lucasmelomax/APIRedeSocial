
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Likes.Queries.GetLikesById
{
    public class GetLikesByIdQueryHandler : IRequestHandler<GetLikesByIdQuery, LikesDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public GetLikesByIdQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<LikesDTO> Handle(GetLikesByIdQuery request, CancellationToken ct)
        {
            if (request.id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var likes = await _uof.LikesRepository.GetById(request.id, ct);
            var likesDTO = _mapper.Map<LikesDTO>(likes);
            if (likes is null)
                throw new KeyNotFoundException($"Like com ID {request.id} não encontrado.");
            return likesDTO;
        }
    }
}
