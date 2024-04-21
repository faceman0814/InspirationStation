﻿namespace FaceMan.Utils.Timing;

public class UtcClockProvider : IClockProvider
{
    public DateTime Now => DateTime.UtcNow;

    public DateTimeKind Kind => DateTimeKind.Utc;

    public bool SupportsMultipleTimezone => true;

    public DateTime Normalize(DateTime dateTime)
    {
        // 如果 dateTime 是本地时间，则转为 UTC 时间
        if (dateTime.Kind == DateTimeKind.Unspecified)
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        return dateTime.Kind == DateTimeKind.Local ? dateTime.ToUniversalTime() : dateTime;
    }

    internal UtcClockProvider()
    {
    }
}