using System.Text.Json.Serialization;

namespace ToDoCalendarWeb.Domain;

public class Task : AbstractTrackableEntity
{
    public override Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; } = "";
    public required Guid PeriodId { get; set; }
    public bool IsDraft { get; set; }
    public bool IsCompleted { get; set; }
    [JsonIgnore]
    public override bool IsTracked { get; set; } = true;
}
