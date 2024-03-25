namespace ToDoCalendarWeb.Domain;

public class TimePeriod
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public bool Equals(TimePeriod? timePeriod)
    {
        if(timePeriod == null)
            return false;
        if(timePeriod == this) 
            return true;

        return timePeriod.StartTime == StartTime && timePeriod.EndTime == EndTime;
    }

    public override bool Equals(object? obj)
    {
        if(obj == null)
            return false;
        if(obj == this) 
            return true;
        if(obj.GetType() != typeof(TimePeriod))
            return false;
        return Equals((TimePeriod?)obj);
    }

    public override int GetHashCode()
    {
        return StartTime.GetHashCode() ^ EndTime.GetHashCode();
    }
}
