
namespace RedeSocial.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T?> GetAll();
        Task<T?> GetById(int id, CancellationToken? ct);
        Task<T?> Create(T entity, CancellationToken? ct);
        Task<T?> Update(int id, T entity, CancellationToken? ct);
        Task DeleteById(int id, CancellationToken? ct);
    }
}
