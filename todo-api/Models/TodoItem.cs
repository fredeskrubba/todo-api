using System.ComponentModel.DataAnnotations;

namespace todo_api.Models
{
    public class TodoItem
    {
        
        public long Id { get; set; }

        public long UserId { get; set; }
        
        // cascading deletion support

        public User? User { get; set; } = null!;

        public string Description { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsComplete { get; set; } = false;

        [Required]
        public DateTime DueDate { get; set; }

        public long? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
