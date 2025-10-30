using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Models;
using RedeSocial.Infra.Data.Context;


namespace RedeSocial.Infra.Data.Identity {
    public class AuthenticateService : IAuthentication {

        private readonly RedeSocialContext _context;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public AuthenticateService(RedeSocialContext context, Microsoft.Extensions.Configuration.IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticationAsync(string email, string senha) {
            var usuario = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null) {
                return false;
            }
            var password = await _context.Users.Where(x => x.Password == senha).FirstOrDefaultAsync();
            if (password == null) {
                return false;
            }
            return true;
        }

        public string GenerateToken(int id, string email) {
            var claims = new[]
            {
        new Claim("id", id.ToString()),
        new Claim("email", email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"])
            );

            var credential = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],        
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Users> GetUserByEmail(string email) {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email) {
            var usuario = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null) {
                return false;
            }
            return true;
        }
    }
}
