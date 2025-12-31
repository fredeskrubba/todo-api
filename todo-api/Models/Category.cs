namespace todo_api.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
