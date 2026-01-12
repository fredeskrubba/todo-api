using todo_api.Models.Dtos;
using todo_api.Models;

namespace todo_api.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(CreateUserDTO request);
        Task<LoginResponseDTO?> LoginAsync(LoginDTO request);
    }
}
