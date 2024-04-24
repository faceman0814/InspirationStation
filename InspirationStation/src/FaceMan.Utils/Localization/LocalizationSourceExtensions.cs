﻿using System.Globalization;

namespace FaceMan.Utils.Localization;

public static class LocalizationSourceExtensions
{
    /// <summary>Get a localized string by formatting string.</summary>
    /// <param name="source">Localization source</param>
    /// <param name="name">Key name</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted and localized string</returns>
    public static string GetString(
        this ILocalizationSource source,
        string name,
        params object[] args)
    {
        if (source == null)
            throw new ArgumentNullException(nameof (source));
        return string.Format(source.GetString(name), args);
    }

    /// <summary>
    /// Get a localized string in given language by formatting string.
    /// </summary>
    /// <param name="source">Localization source</param>
    /// <param name="name">Key name</param>
    /// <param name="culture">Culture</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Formatted and localized string</returns>
    public static string GetString(
        this ILocalizationSource source,
        string name,
        CultureInfo culture,
        params object[] args)
    {
        if (source == null)
            throw new ArgumentNullException(nameof (source));
        return string.Format(source.GetString(name, culture), args);
    }
}