using System.Text.Json.Serialization;

namespace ToDoCalendarWeb.Domain;

public class Routine : AbstractTrackableEntity 
{
    public override Guid Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public virtual List<Period>? Periods { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public override bool IsTracked { get; set; } = true;

}
