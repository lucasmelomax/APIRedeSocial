﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Create(T entity);
        Task<T?> Update(T entity);
        Task DeleteById(int id);
    }
}
