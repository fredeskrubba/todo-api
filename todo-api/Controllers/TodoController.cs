using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var todos = new List<TodoItem>
        {
            new TodoItem
            {
                Id = 1,
                Title = "Learn ASP.NET Core",
                Description = "Understand MVC and Web APIs",
                IsComplete = false
            },
            new TodoItem
            {
                Id = 2,
                Title = "Build Todo API",
                Description = "Create controllers and endpoints",
                Color = "#f9c74f",
                IsComplete = false
            },
            new TodoItem
            {
                Id = 3,
                Title = "Push to GitHub",
                Description = "Create repository and commit code",
                Color = "#f94144",
                IsComplete = true
            }
        };

            return Ok(todos);
        }
    }


}
