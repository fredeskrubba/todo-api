using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsersAsync();
        public Task<UserDTO> GetUserAsync(long id);
        public Task<UserDTO> UpdateUserAsync(long id, UserDTO user);
        public Task<bool> DeleteUserAsync(long id);
    }
}
