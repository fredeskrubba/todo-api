using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Services;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly TodoContext _context;


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO createdCategory)
        {


            var userDTO = await categoryService.CreateCategoryAsync(createdCategory);

            return Ok(userDTO);

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCategories(int userId)
        {
            var result = await categoryService.GetCategoriesAsync(userId);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, CategoryDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await categoryService.UpdateCategoryAsync(id, item);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);

            return Ok(result);
        }
    
    }
}
