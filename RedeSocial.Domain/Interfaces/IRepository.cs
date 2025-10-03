using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T?> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Create(T entity);
        Task<T?> Update(int id, T entity);
        Task DeleteById(int id);
    }
}
