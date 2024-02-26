using System.Text.Json.Serialization;

namespace ToDoCalendarWeb.Domain;

public abstract class AbstractTrackableEntity : AbstractEntity
{
    [JsonIgnore]
    public abstract bool IsTracked { get; set; }

}
