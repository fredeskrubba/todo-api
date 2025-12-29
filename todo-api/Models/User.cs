
using System.ComponentModel.DataAnnotations;

namespace todo_api.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
