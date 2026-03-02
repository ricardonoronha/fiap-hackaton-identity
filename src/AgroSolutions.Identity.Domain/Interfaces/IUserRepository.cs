using AgroSolutions.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByEmailAsync(string email);
        public Task<User?> GetByCredentialsAsync(string email, string password);
        public Task AddAsync(User user);
        public Task RemoveAsync(string email);
    }
}
