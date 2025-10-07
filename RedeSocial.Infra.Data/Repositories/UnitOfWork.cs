using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Infra.Data.Context;

namespace RedeSocial.Infra.Data.Repositories {
    public class UnitOfWork : IUnitOfWork {

        private readonly RedeSocialContext _context;
        private IRepository<Users> _userRepository;

        public UnitOfWork(RedeSocialContext context) {
            _context = context;
        }

        public IRepository<Users> UserRepository
            => _userRepository ??= new Repository<Users>(_context);

        public async Task<bool> Commit() {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}