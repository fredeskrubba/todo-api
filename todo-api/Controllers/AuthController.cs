using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Services;


namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
      
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(CreateUserDTO request)
        {
            var user = await authService.RegisterAsync(request);

            if(user == null)
            {
                return BadRequest("User Already exists");
            }

            return Ok(user);
        }

        [EnableRateLimiting("LoginPolicy")]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO request)
        {
            
            LoginResponseDTO response = await authService.LoginAsync(request);
            if(response.Token == null)
            {
                return BadRequest("Invalid Credentials");
            }

            return Ok(response);
        }


        [EnableRateLimiting("GuestLoginPolicy")]
        [HttpPost("guest")]
        public async Task<IActionResult> GuestLogin()
        {
            var response = await authService.GuestLoginAsync();
            return Ok(response);
        }


    }
}
