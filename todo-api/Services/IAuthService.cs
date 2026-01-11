using todo_api.Models.Dtos;
using todo_api.Models;

namespace todo_api.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
    }
}
