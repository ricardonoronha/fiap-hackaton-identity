using AgroSolutions.Application.Interfaces;
using AgroSolutions.Identity.Domain.Interfaces;
using AgroSolutions.Identity.DTOs;
using AgroSolutions.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSolutions.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUserAsync(UserInputDto user)
        {
            await userRepository.AddAsync(new User
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password // In a real application, you should hash the password before storing it
            });
        }

        public async Task<List<UserOutputDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            var dtos = users.Select(u => new UserOutputDto
            {
                Email = u.Email,
                Name = u.Name,
            }).ToList();

            return dtos;
        }

        public async Task<UserOutputDto> GetByCredentials(string email, string password)
        {
            var user = await userRepository.GetByCredentialsAsync(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserOutputDto
            {
                Email = user.Email,
                Name = user.Name,
            };
        }
    }
}
