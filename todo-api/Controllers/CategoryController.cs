using Microsoft.AspNetCore.Http;
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
    public class CategoryController : ControllerBase
    {
        private readonly TodoContext _context;

        public CategoryController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO createdCategory)
        {

            var user = await _context.Users.FindAsync(1);
     

            Category category = new Category()
            {
                Id = createdCategory.Id,
                UserId = createdCategory.UserId,
                Name = createdCategory.Name,
                Color = createdCategory.Color,
                User = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };


            
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            CategoryDTO userDTO = new CategoryDTO()
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt

            };

            return Ok(userDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _context.Categories
            .Select(category => new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt


            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt

            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, CategoryDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }


            category.Id = item.Id;
            category.UserId = item.UserId;
            category.Name = item.Name;
            category.Color = item.Color;

            category.UpdatedAt = DateTime.UtcNow;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The user was modified by another process.");
            }


            var result = new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            var result = new CategoryDTO
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = category.Name,
                Color = category.Color,

                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            return Ok(result);
        }
    
    }
}
