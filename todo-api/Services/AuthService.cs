using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public class AuthService(TodoContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<User?> RegisterAsync(CreateUserDTO request)
        {
            if(await context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO request)
        {
            
            
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            UserDTO LoggedInUser = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Role = user.Role
            };

            LoginResponseDTO response = new()
            {
                user = LoggedInUser,
                Token = CreateToken(user)
            };

            return response;
        }

        public async Task<LoginResponseDTO> GuestLoginAsync()
        {
            

            var guestUser = new User
            {
                FirstName = "Guest",
                LastName = "User",
                Email = $"guest-{Guid.NewGuid()}@guest.local",
                Role = "Guest",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHash = ""
            };


            context.Users.Add(guestUser);
            await context.SaveChangesAsync();

            UserDTO guestDto = new()
            {
                Id = guestUser.Id,
                FirstName = guestUser.FirstName,
                LastName = guestUser.LastName,
                Email = guestUser.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = "Guest"
            };

            return new LoginResponseDTO
            {
                user = guestDto,
                Token = CreateToken(guestUser)
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.GetFullName()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: user.Role == "Guest"
                ? DateTime.UtcNow.AddMinutes(15)
                : DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
