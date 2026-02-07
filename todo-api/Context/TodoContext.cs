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

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TodoItem>()
               .HasOne(t => t.Category)
               .WithMany(c => c.TodoItems)
               .HasForeignKey(t => t.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
               .HasOne(n => n.User)
               .WithMany(u => u.Notes)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
