using System.Text.Json.Serialization;

namespace ToDoCalendarWeb.Domain;

public class Period : AbstractTrackableEntity
{
    public override Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid RoutineId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    [JsonIgnore]
    public virtual List<Task>? Tasks { get; set; }
    public virtual required TimePeriod TimePeriod { get; set; }
    [JsonIgnore]
    public override bool IsTracked { get; set; } = true;
}
