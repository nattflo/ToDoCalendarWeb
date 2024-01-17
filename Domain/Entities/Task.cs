namespace ToDoCalendarWeb.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Guid PeriodId { get; set; }
        public bool IsDraft { get; set; }
        public bool IsCompleted { get; set; }
    }
}
