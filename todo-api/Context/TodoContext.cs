namespace todo_api.Context
{
    using Microsoft.EntityFrameworkCore;
    using todo_api.Models;
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoListItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
