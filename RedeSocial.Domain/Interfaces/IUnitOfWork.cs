using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Interfaces {
    public interface IUnitOfWork : IDisposable {
        IRepository<Users> UserRepository { get; }
        IRepository<Posts> PostsRepository { get; }
        IRepository<PostsPhotos> PostsPhotosRepository { get; }
        IRepository<Comments> CommentsRepository { get; }
        IRepository<Likes> LikesRepository { get; }
        IRepository<Followers> FollowersRepository { get; }
        Task<bool> Commit();
    }
}