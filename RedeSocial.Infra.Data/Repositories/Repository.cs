using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Pagination;
using RedeSocial.Infra.Data.Context;
using RedeSocial.Infra.Data.Helpers;

namespace RedeSocial.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
    
        private readonly RedeSocialContext _context;

        private readonly DbSet<T> _dbSet;
        public Repository(RedeSocialContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T?> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public async Task<T> Update(int id, T entity) {
            var update = await _dbSet.FindAsync(id);
            _dbSet.Update(entity);
            return entity;
        }
        public async Task DeleteById(int id)
        {
            var deletado = await _dbSet.FindAsync(id);
            if(deletado != null) _dbSet.Remove(deletado);
        }

    }
}
