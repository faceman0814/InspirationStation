using System;

namespace FaceManUtils.Timing;

public interface IClockProvider
{
    /// <summary>
    /// 获取当前时间
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// 获取时间日期种类
    /// </summary>
    DateTimeKind Kind { get; }

    /// <summary>
    /// 提供程序支持多个时区
    /// </summary>
    bool SupportsMultipleTimezone { get; }

    /// <summary>
    /// 标准化时间
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    DateTime Normalize(DateTime dateTime);
}