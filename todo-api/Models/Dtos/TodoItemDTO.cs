namespace todo_api.Models.Dtos
{
    public class TodoItemDTO
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string Color { get; set; } = "#61bd92";
        public string Title { get; set; }
        public bool IsComplete { get; set; } = false;

        public long? CategoryId { get; set; }

        public DateTime DueDate { get; set; }
    }
}
