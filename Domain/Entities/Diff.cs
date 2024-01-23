namespace ToDoCalendarWeb.Domain;

public class Diff
{
    public Guid Id { get; set; }
    public required Guid ObjectId { get; set; }
    public required Type ObjectType { get; set; }
    public required DateTime ChangeTime { get; set; }
    public required string PropName { get; set; }
    public required object From { get; set; }
    public required object To { get; set; }
}
