using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Interfaces {
    public interface IUnitOfWork {
        Task<bool> Commit();
    }
}
