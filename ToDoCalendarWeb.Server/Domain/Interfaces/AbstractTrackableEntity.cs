namespace ToDoCalendarWeb.Domain;

public abstract class AbstractTrackableEntity : AbstractEntity
{
    public abstract bool IsTracked { get; set; }

}
