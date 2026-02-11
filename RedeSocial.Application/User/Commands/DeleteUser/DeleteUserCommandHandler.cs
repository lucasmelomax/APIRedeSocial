
using MediatR;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {

        private readonly IUnitOfWork _uof;

        public DeleteUserCommandHandler(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken ct)
        {

            if (request.id <= 0) throw new KeyNotFoundException("O id precisa ser maior que 0.");

            var user = await _uof.UserRepository.GetById(request.id, ct);

            if (user is null)
                throw new KeyNotFoundException("Usuário não encontrado");

            var userPosts = _uof.PostsRepository
                .GetAll()
                .Where(p => p.UsersId == request.id);

            foreach (var post in userPosts)
            {
                await _uof.PostsRepository.DeleteById(post.PostsId, ct);
            }

            await _uof.UserRepository.DeleteById(request.id, ct);
            await _uof.Commit();

            return Unit.Value;
        }
    }
}
