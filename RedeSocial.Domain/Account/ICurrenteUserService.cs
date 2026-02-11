using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Account
{
    public interface ICurrenteUserService
    {
        int UserId { get; }
    }
}
