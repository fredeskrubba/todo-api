using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _context;

        public UserController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createdUser)
        {
            var hasher = new PasswordHasher<User>();

            User user = new User()
            {
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                IsAdmin = createdUser.IsAdmin,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            user.PasswordHash = hasher.HashPassword(user, createdUser.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                TodoItems = user.TodoItems,
            };

            return Ok(userDTO);

        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            // Placeholder implementation
            return Ok("User endpoint is under construction.");
        }
    }
}
