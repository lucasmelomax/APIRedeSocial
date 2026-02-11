
using AutoMapper;
using MediatR;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Likes.Commands.DeleteLikes
{
    public class DeleteLikesCommandHandler : IRequestHandler<DeleteLikesCommand, Unit>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public DeleteLikesCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteLikesCommand request, CancellationToken ct)
        {
            if (request.id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");

            var deletado = await _uof.LikesRepository.GetById(request.id, ct);

            if (deletado is null) throw new InvalidOperationException("Post invalido.");

            if (deletado.LikesId != request.id) throw new InvalidOperationException("Id invalido.");

            await _uof.LikesRepository.DeleteById(request.id, ct);

            await _uof.Commit();

            return Unit.Value;
        }
    }
}
