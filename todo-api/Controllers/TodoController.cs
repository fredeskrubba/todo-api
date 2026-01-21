using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;
using todo_api.Services;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
       

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItem item, long userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            TodoItemDTO result = await todoService.CreateItemAsync( item, userId);


            return Ok(result);

        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetItems(int userId)
        {

            var items = await todoService.GetTodoItemsAsync(userId);

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

            TodoItemDTO result;

            try
            {
                result = await todoService.UpdateItemAsync(id, item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var result = await todoService.DeleteItemAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            

            return Ok(result);
        }
    }


}
