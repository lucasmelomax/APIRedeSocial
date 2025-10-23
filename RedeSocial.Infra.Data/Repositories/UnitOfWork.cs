using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Infra.Data.Context;

namespace RedeSocial.Infra.Data.Repositories {
    public class UnitOfWork : IUnitOfWork {

        private readonly RedeSocialContext _context;
        private IRepository<Users> _userRepository;
        private IRepository<Posts> _postsRepository;
        private IRepository<PostsPhotos> _postsPhotosRepository;
        private IRepository<Comments> _commentsRepository;
        private IRepository<Likes> _likesRepository;
        private IRepository<Followers> _followersRepository;

        public UnitOfWork(RedeSocialContext context) {
            _context = context;
        }

        public IRepository<Users> UserRepository
            => _userRepository ??= new Repository<Users>(_context);

        public IRepository<Posts> PostsRepository
           => _postsRepository ??= new Repository<Posts>(_context);
        public IRepository<PostsPhotos> PostsPhotosRepository
           => _postsPhotosRepository ??= new Repository<PostsPhotos>(_context);

        public IRepository<Comments> CommentsRepository 
            => _commentsRepository ??= new Repository<Comments>(_context);

        public IRepository<Likes> LikesRepository
            => _likesRepository ??= new Repository<Likes>(_context);


        public IRepository<Followers> FollowersRepository
            => _followersRepository ??= new Repository<Followers>(_context);

        public async Task<bool> Commit() {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}