namespace todo_api.Models.Dtos
{
    public class NoteDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }

        public string Color { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
