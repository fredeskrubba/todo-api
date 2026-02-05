using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public class TodoService(TodoContext context, IConfiguration configuration) : ITodoService
    {
        public async Task<TodoItemDTO> CreateItemAsync(TodoItem item, long userId)
        {
            context.TodoListItems.Add(item);
            try
            {

                await context.SaveChangesAsync();
            } catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while trying to create the todo item", dbEx);
            }

            TodoItemDTO createdItem = new TodoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsComplete = item.IsComplete,
                DueDate = item.DueDate,
                CategoryId = item.CategoryId
            };

            return createdItem;
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            TodoItem todoItem = await context.TodoListItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException("Todo item not found");
            }


            context.TodoListItems.Remove(todoItem);
            bool result;
            try
            {
                await context.SaveChangesAsync();
                result = true;
            }
            catch (DbUpdateException dbEx)
            {
                result = false;
                throw new Exception("An error occurred while trying to delete the todo item", dbEx);

            }

 

            return result;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync(int userId)
        {   

            var items = await context.TodoListItems
                .Where(item => item.UserId == userId)
                .Select(item => new TodoItemDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    IsComplete = item.IsComplete,
                    DueDate = item.DueDate,
                    CategoryId = item.CategoryId
                })
                .ToListAsync();

            return items;
        }

        public async Task<TodoItemDTO> UpdateItemAsync(long id, TodoItemDTO item)
        {
            var itemToUpdate = await context.TodoListItems.FindAsync(id);

            itemToUpdate.Title = item.Title;
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsComplete = item.IsComplete;
            itemToUpdate.DueDate = item.DueDate;
            itemToUpdate.CategoryId = item.CategoryId;

            context.Entry(itemToUpdate).State = EntityState.Modified;

            await context.SaveChangesAsync();
           

            var result = new TodoItemDTO()
            {
                Title = itemToUpdate.Title,
                Id = itemToUpdate.Id,
                Description = itemToUpdate.Description,
                IsComplete = itemToUpdate.IsComplete,
                DueDate = itemToUpdate.DueDate,
                CategoryId = itemToUpdate.CategoryId
            };

            return result;
        }
    }
}
