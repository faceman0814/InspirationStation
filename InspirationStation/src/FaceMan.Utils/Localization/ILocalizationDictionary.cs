using System.Globalization;
using Microsoft.Extensions.Localization;

namespace FaceMan.Utils.Localization;

public interface ILocalizationDictionary
{
    CultureInfo CultureInfo { get; }

    /// <summary>
    /// Gets/sets a string for this dictionary with given name (key).
    /// </summary>
    /// <param name="name">Name to get/set</param>
    string this[string name] { get; set; }

    /// <summary>
    /// Gets a <see cref="T:Abp.Localization.LocalizedString" /> for given <paramref name="name" />.
    /// </summary>
    /// <param name="name">Name (key) to get localized string</param>
    /// <returns>The localized string or null if not found in this dictionary</returns>
    LocalizedString GetOrNull(string name);

    /// <summary>
    /// Gets a <see cref="T:Abp.Localization.LocalizedString" /> for given <paramref name="names" />.
    /// </summary>
    /// <param name="names">Names (key) to get list of localized strings</param>
    /// <returns>The localized string or null if not found in this dictionary</returns>
    IReadOnlyList<LocalizedString> GetStringsOrNull(List<string> names);

    /// <summary>Gets a list of all strings in this dictionary.</summary>
    /// <returns>List of all <see cref="T:Abp.Localization.LocalizedString" /> object</returns>
    IReadOnlyList<LocalizedString> GetAllStrings();
}