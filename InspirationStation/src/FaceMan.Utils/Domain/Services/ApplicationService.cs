using FaceMan.Utils.Exception;
using FaceMan.Utils.Timing;

namespace FaceMan.Utils.Domain.Services;

public class ApplicationService:
    ServiceBase,
    IApplicationService
    // IAvoidDuplicateCrossCuttingConcerns
{
    protected void ThrowUserFriendlyError(string reason)
    {
        throw new UserFriendlyException(L("Error"),
            L("UserFriendlyError", reason, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        );
    }
}