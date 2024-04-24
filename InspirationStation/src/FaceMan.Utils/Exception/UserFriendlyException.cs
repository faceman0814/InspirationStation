using System.Runtime.Serialization;

namespace FaceMan.Utils.Exception;

public class UserFriendlyException : InspirationStationException, IHasErrorCode
{
    /// <summary>
    ///  设置默认的日志级别
    /// </summary>
    public static LogSeverity DefaultLogSeverity = LogSeverity.Warn;

    /// <summary>有关异常的其他信息。</summary>
    public string Details { get; private set; }

    /// <summary>An arbitrary error code.</summary>
    public int Code { get; set; }

    /// <summary>
    /// Severity of the exception.
    /// Default: Warn.
    /// </summary>
    public LogSeverity Severity { get; set; }

    /// <summary>Constructor.</summary>
    public UserFriendlyException() => this.Severity = UserFriendlyException.DefaultLogSeverity;

    /// <summary>Constructor for serializing.</summary>
    public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    public UserFriendlyException(string message)
        : base(message)
    {
        this.Severity = UserFriendlyException.DefaultLogSeverity;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="severity">Exception severity</param>
    public UserFriendlyException(string message, LogSeverity severity)
        : base(message)
    {
        this.Severity = severity;
    }

    /// <summary>Constructor.</summary>
    /// <param name="code">Error code</param>
    /// <param name="message">Exception message</param>
    public UserFriendlyException(int code, string message)
        : this(message)
    {
        this.Code = code;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    public UserFriendlyException(string message, string details)
        : this(message)
    {
        this.Details = details;
    }

    /// <summary>Constructor.</summary>
    /// <param name="code">Error code</param>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    public UserFriendlyException(int code, string message, string details)
        : this(message, details)
    {
        this.Code = code;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public UserFriendlyException(string message, System.Exception innerException)
        : base(message, innerException)
    {
        this.Severity = UserFriendlyException.DefaultLogSeverity;
    }

    /// <summary>Constructor.</summary>
    /// <param name="message">Exception message</param>
    /// <param name="details">Additional information about the exception</param>
    /// <param name="innerException">Inner exception</param>
    public UserFriendlyException(string message, string details, System.Exception innerException)
        : this(message, innerException)
    {
        this.Details = details;
    }
}