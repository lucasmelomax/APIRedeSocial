
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.PostsPhotos.Command.DeletePostPhotos;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.PostsPhotos.Command.DeletePostsPhotos
{
    public class DeletePostsPhotosCommandHandler : IRequestHandler<DeletePostsPhotosCommand, Unit>
    {

        private readonly IUnitOfWork _uof;

        public DeletePostsPhotosCommandHandler(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Unit> Handle(DeletePostsPhotosCommand request, CancellationToken ct)
        {
            var deletado = await _uof.PostsPhotosRepository.GetById(request.id, ct);

            if (deletado is not null && deletado.PostsPhotosId != request.id) throw new InvalidOperationException("Post Photo invalido.");

            await _uof.PostsPhotosRepository.DeleteById(request.id, ct);

            await _uof.Commit();

            return Unit.Value;
        }
    }
}
