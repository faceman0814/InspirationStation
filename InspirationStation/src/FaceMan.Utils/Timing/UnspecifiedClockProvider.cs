using System;

namespace FaceManUtils.Timing;

public class UnspecifiedClockProvider : IClockProvider
{
    public DateTime Now => DateTime.Now;

    public DateTimeKind Kind => DateTimeKind.Unspecified;

    public bool SupportsMultipleTimezone => false;

    public DateTime Normalize(DateTime dateTime) => dateTime;

    internal UnspecifiedClockProvider()
    {
    }
}