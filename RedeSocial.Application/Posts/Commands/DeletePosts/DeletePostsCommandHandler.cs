
using MediatR;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Commands.DeletePosts
{
    public class DeletePostsCommandHandler : IRequestHandler<DeletePostsCommand, Unit>
    {
        private readonly IUnitOfWork _uof;

        public DeletePostsCommandHandler(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<Unit> Handle(DeletePostsCommand request, CancellationToken ct)
        {
            if (request.id <= 0) throw new InvalidOperationException("O id precisa ser maior que 0.");

            var deletado = await _uof.PostsRepository.GetById(request.id, ct);

            if (deletado is null) throw new InvalidOperationException("Post invalido.");

            await _uof.PostsRepository.DeleteById(request.id, ct);

            await _uof.Commit();

            return Unit.Value;
        }
    }
}
