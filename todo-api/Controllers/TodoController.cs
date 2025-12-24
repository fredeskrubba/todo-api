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
        public async Task<IActionResult> CreateItem(TodoItem item)
        {

            _context.TodoListItems.Add(item);
            await _context.SaveChangesAsync();

            TodoItemDTO result = new TodoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Color = item.Color,
                IsComplete = item.IsComplete
            };
            return Ok(result);

        }


        [HttpGet]
        public async Task<IActionResult> GetItems()
        {

            var result = await _context.TodoListItems
            .Select(item => new TodoItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Color = item.Color,
                IsComplete = item.IsComplete

            }).ToListAsync();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(long id)
        {

         
           var item = await _context.TodoListItems.FindAsync(id);

           if(item == null)
            {
                return NotFound();
            }

            var result = new TodoItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Color = item.Color,
                IsComplete = item.IsComplete
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(long id, TodoItem item)
        {

            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            var updatedItem = await _context.TodoListItems.FindAsync(id);

            var result = new TodoItemDTO
            {
                Id = updatedItem.Id,
                Title = updatedItem.Title,
                Description = updatedItem.Description,
                Color = updatedItem.Color,
                IsComplete = updatedItem.IsComplete
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
                IsComplete = todoItem.IsComplete
            };

            return Ok(result);
        }
    }


}
