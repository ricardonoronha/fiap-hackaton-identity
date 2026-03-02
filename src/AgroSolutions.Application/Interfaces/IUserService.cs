using AgroSolutions.Identity.DTOs;


namespace AgroSolutions.Application.Interfaces
{
    public interface IUserService
    {

        public Task<List<UserOutputDto>> GetAllUsersAsync();
        public Task<UserOutputDto> GetByCredentials(string email, string password);
        public Task AddUserAsync(UserInputDto user);
    }
}
