using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Interfaces {
    public interface IUnitOfWork : IDisposable {
        IRepository<Users> UserRepository { get; }
        Task<bool> Commit();
    }
}