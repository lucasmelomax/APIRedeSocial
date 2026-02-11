using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RedeSocial.Domain.Account;

namespace RedeSocial.Infra.Data.Identity
{
    public class CurrenteUserService : ICurrenteUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrenteUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("Usuário não autenticado.");

                return int.Parse(userId);
            }
        }
    }
}
