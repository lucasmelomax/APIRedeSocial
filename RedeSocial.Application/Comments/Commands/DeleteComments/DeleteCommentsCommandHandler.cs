
using AutoMapper;
using MediatR;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Comments.Commands.DeleteComments
{
    public class DeleteCommentsCommandHandler : IRequestHandler<DeleteCommentsCommand, Unit>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public DeleteCommentsCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteCommentsCommand request, CancellationToken ct)
        {
            if (request.id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var deletado = await _uof.CommentsRepository.GetById(request.id, ct);
            if (deletado is null) throw new InvalidOperationException("Post invalido.");
            if (deletado.CommentsId != request.id) throw new InvalidOperationException("Id invalido.");
            await _uof.CommentsRepository.DeleteById(request.id, ct);
            await _uof.Commit();
            return Unit.Value;
        }
    }
}
