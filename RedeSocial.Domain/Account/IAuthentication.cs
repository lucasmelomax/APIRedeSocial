using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Domain.Models;

namespace RedeSocial.Domain.Account {
    public interface IAuthentication {

        Task<bool> AuthenticationAsync(string email, string senha);
        Task<bool> UserExists(string email);
        public string GenerateToken(int id, string email);
        public Task<Users> GetUserByEmail(string email);
    }
}
