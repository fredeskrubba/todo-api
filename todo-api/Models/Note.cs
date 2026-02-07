namespace todo_api.Models
{
    public class Note
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }

        public string Color { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
