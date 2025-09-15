using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Infra.Data.Context;

namespace RedeSocial.Infra.Data.Repositories {
    public class UnitOfWork : IUnitOfWork {

        public RedeSocialContext _context;

        public UnitOfWork(RedeSocialContext context) {
            _context = context;
        }

        public async Task<bool> Commit() {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
