using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public class UserService(TodoContext context, IConfiguration configuration) : IUserService
    {
        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("user not found");
            }

            context.Users.Remove(user);

            try
            {

                await context.SaveChangesAsync();
            } catch
            {
                throw new Exception("Something went wrong updating user");
            }

            

            return true;
        }

        public async Task<UserDTO> GetUserAsync(long id)
        {
            var item = await context.Users.FindAsync(id);

            if (item == null)
            {
                throw new Exception("item not found");
            }

            var result = new UserDTO
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,

            };

            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var result = await context.Users
            .Select(item => new UserDTO
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,


            }).ToListAsync();

            if(result == null)
            {
                throw new Exception("no users found");
            }

            return result;
        }

        public async Task<UserDTO> UpdateUserAsync(long id, UserDTO user)
        {
            var updatedUser = await context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }


            user.FirstName = user.FirstName;
            user.LastName = user.LastName;
            user.Email = user.Email;
            user.UpdatedAt = DateTime.UtcNow;

            try
            {

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Error updating user");
            }


            var result = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };

            return result;
        }
    }
}
