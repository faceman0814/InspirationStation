namespace FaceMan.Utils.Timing;

public class LocalClockProvider : IClockProvider
{
    public DateTime Now => DateTime.Now;

    public DateTimeKind Kind => DateTimeKind.Local;

    public bool SupportsMultipleTimezone => false;

    public DateTime Normalize(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        return dateTime.Kind == DateTimeKind.Utc ? dateTime.ToLocalTime() : dateTime;
    }

    internal LocalClockProvider()
    {
    }
}