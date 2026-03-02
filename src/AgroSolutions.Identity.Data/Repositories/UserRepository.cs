using AgroSolutions.Identity.Domain.Interfaces;
using AgroSolutions.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Identity.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _ctx;

        public UserRepository(AuthContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(User user)
        {
            if (user is not null)
            {
                _ctx.Users.Add(user);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = _ctx.Users.ToList();
            return users;
        }

        public async Task<User?> GetByCredentialsAsync(string email, string password)
        {
            if (email is null || password is null)
                throw new ArgumentException("Email and password cannot be null.");

            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null)
                throw new ArgumentException("Email cannot be null.");

            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task RemoveAsync(string email)
        {
            if (email is null)
                throw new ArgumentException("Email cannot be null.");

            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is not null)
            {
                _ctx.Users.Remove(user);
                await _ctx.SaveChangesAsync();
            }

        }
    }
}
