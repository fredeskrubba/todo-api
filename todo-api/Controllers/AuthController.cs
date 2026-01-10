using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_api.Models;
using todo_api.Models.Dtos;


namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new User();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            user.FirstName = "test";
            user.LastName = "user";
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;
            user.Email = request.Email;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;  

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO request)
        {
            user.FirstName = "test";
            user.LastName = "user";
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, "secret");
            user.PasswordHash = hashedPassword;
            user.Email = request.Email;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            if (user.GetFullName() != request.FirstName + " " + request.LastName)
            {
                return BadRequest("User doesn't exist: " + request.FirstName + " " + request.LastName);
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.GetFullName())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
