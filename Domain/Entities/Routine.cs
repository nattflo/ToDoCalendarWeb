namespace ToDoCalendarWeb.Domain;

public class Routine
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<Period> Periods { get; set; }
    public string? Description { get; set; }

}
