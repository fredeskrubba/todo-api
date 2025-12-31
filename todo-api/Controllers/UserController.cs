using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            };

            return Ok(userDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _context.Users
            .Select(item => new UserDTO
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                IsAdmin = item.IsAdmin,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                

            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var item = await _context.Users.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var result = new UserDTO
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                IsAdmin = item.IsAdmin,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,

            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, UserDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.Email = item.Email;
            user.IsAdmin = item.IsAdmin;
            user.UpdatedAt = DateTime.UtcNow;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The user was modified by another process.");
            }

            
            var result = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            var result = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };

            return Ok(result);
        }
    }
}
