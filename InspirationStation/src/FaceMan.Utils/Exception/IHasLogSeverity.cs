namespace FaceMan.Utils.Exception;

public interface IHasLogSeverity
{
    LogSeverity Severity { get; set; }
}