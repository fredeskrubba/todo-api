using System.ComponentModel.DataAnnotations;

namespace todo_api.Models
{
    public class TodoItem
    {
        
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Description { get; set; }

        public string Color { get; set; } = "#61bd92";
        public string Title { get; set; }
        public bool IsComplete { get; set; } = false;

        public DateTime DueDate { get; set; }
    }
}
