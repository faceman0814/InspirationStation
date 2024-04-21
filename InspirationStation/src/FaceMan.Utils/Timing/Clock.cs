using System;

namespace FaceManUtils.Timing;

public static class Clock
{
    private static IClockProvider _provider;

    /// <summary>
    /// 这个对象是用来执行所有 <see cref="T:FaceManUtils.Timing.Clock" /> 操作.
    /// Default value: <see cref="T:FaceManUtils.Timing.UnspecifiedClockProvider" />.
    /// </summary>
    public static IClockProvider Provider
    {
        get => Clock._provider;
        set
        {
            Clock._provider = value != null ? value : throw new ArgumentNullException(nameof (value), "Can not set Clock.Provider to null!");
        }
    }

    /// <summary>
    /// 初始化时钟。
    /// </summary>
    static Clock() => Clock.Provider = (IClockProvider) ClockProviders.Unspecified;

    /// <summary>
    /// 获取当前时间。
    /// </summary>
    public static DateTime Now => Clock.Provider.Now;
    
    /// <summary>
    /// 获取当前时间的Kind。
    /// </summary>
    public static DateTimeKind Kind => Clock.Provider.Kind;

    /// <summary>
    /// 如果支持多个时区，则返回true；如果不支持，则返回false。
    /// </summary>
    public static bool SupportsMultipleTimezone => Clock.Provider.SupportsMultipleTimezone;

    /// <summary>
    /// 规范化日期时间
    /// </summary>
    /// <param name="dateTime">待规范化的日期时间.</param>
    /// <returns>标准化日期时间</returns>
    public static DateTime Normalize(DateTime dateTime) => Clock.Provider.Normalize(dateTime);
}