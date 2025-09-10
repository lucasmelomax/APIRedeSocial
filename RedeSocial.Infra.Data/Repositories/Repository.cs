using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Interfaces;


using RedeSocial.Infra.Data.Context;

namespace RedeSocial.Infra.Data.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
    
        private readonly RedeSocialContext _context;

        private readonly DbSet<T> _dbSet;

        public Repository(RedeSocialContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T?> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T?> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteById(int id)
        {
            var deletado = await GetById(id);
            if(deletado != null) _dbSet.Remove(deletado);
            await _context.SaveChangesAsync();
        }
    }
}
