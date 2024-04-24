using System.Collections;

namespace FaceMan.Utils.Localization;

public interface ILocalizationSourceList:
    IList<ILocalizationSource>,
    ICollection<ILocalizationSource>,
    IEnumerable<ILocalizationSource>,
    IEnumerable
{
    IList<LocalizationSourceExtensionInfo> Extensions { get; }
}