using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItem item, long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           

            _context.TodoListItems.Add(item);
            await _context.SaveChangesAsync();

            TodoItemDTO result = new TodoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Color = item.Color,
                IsComplete = item.IsComplete,
                DueDate = item.DueDate,
                CategoryId = item.CategoryId
            };
            return Ok(result);

        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetItems(int userId)
        {
            var items = await _context.TodoListItems
                .Where(item => item.UserId == userId)
                .Select(item => new TodoItemDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Color = item.Color,
                    IsComplete = item.IsComplete,
                    DueDate = item.DueDate,
                    CategoryId = item.CategoryId
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, TodoItemDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            var itemToUpdate = await _context.TodoListItems.FindAsync(id);

            itemToUpdate.Title = item.Title;
            itemToUpdate.Description = item.Description;
            itemToUpdate.Color = item.Color;
            itemToUpdate.IsComplete = item.IsComplete;
            itemToUpdate.DueDate = item.DueDate;
            itemToUpdate.CategoryId = item.CategoryId;

            _context.Entry(itemToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            var result = new TodoItemDTO()
            {
                Title = itemToUpdate.Title,
                Id = itemToUpdate.Id,
                Description = itemToUpdate.Description,
                Color = itemToUpdate.Color,
                IsComplete = itemToUpdate.IsComplete,
                DueDate = itemToUpdate.DueDate,
                CategoryId = itemToUpdate.CategoryId
            };

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var todoItem = await _context.TodoListItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoListItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            var result = new TodoItemDTO
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Color = todoItem.Color,
                IsComplete = todoItem.IsComplete,
                DueDate = todoItem.DueDate
            };

            return Ok(result);
        }
    }


}
