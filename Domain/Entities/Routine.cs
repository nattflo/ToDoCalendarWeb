namespace ToDoCalendarWeb.Domain;

public class Routine : AbstractTrackableEntity 
{
    public override Guid Id { get; set; }
    public required string Name { get; set; }
    public List<Period>? Periods { get; set; }
    public string? Description { get; set; }
    public override bool IsTracked { get; set; } = true;

}
