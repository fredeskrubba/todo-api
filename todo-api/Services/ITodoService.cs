using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public interface ITodoService
    {
        Task<TodoItemDTO> CreateItemAsync(TodoItem item, long userId);
        Task<TodoItemDTO> UpdateItemAsync(long id, TodoItemDTO item);
        Task<bool> DeleteItemAsync(long id);
        Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync(int userId);
    }
}
