using FaceMan.Utils.Exception;
using FaceMan.Utils.Timing;

namespace FaceMan.Utils.Domain.Services;

public class DomainService: ServiceBase, IDomainService
{
    /// <summary>
    /// 本地化资源名称
    /// </summary>
    protected string LocalizationSourceName { get; set; }
    
    /// <summary>
    /// 抛出 ThrowUserFriendlyError 异常
    /// </summary>
    protected virtual void ThrowUserFriendlyError(string reason)
    {
        throw new UserFriendlyException(L("Error"),
            L("UserFriendlyError", reason, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        );
    }

    /// <summary>
    /// 抛出 RepetError 异常
    /// </summary>
    /// <param name="name">Entity的名称</param>
    protected virtual void ThrowRepetError(string name)
    {
        throw new UserFriendlyException(L("Error"),
            L("RepetError", name, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        );
    }
}