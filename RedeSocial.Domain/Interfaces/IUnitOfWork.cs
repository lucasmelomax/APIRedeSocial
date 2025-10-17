using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Interfaces {
    public interface IUnitOfWork : IDisposable {
        IRepository<Users> UserRepository { get; }
        IRepository<Posts> PostsRepository { get; }
        IRepository<PostsPhotos> PostsPhotosRepository { get; }
        Task<bool> Commit();
    }
}