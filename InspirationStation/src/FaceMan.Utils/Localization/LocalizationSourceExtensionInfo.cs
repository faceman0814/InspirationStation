﻿namespace FaceMan.Utils.Localization;

public class LocalizationSourceExtensionInfo
{
    /// <summary>Source name.</summary>
    public string SourceName { get; private set; }

    /// <summary>Extension dictionaries.</summary>
    public ILocalizationDictionaryProvider DictionaryProvider { get; private set; }

    /// <summary>
    /// Creates a new <see cref="T:Abp.Localization.Sources.LocalizationSourceExtensionInfo" /> object.
    /// </summary>
    /// <param name="sourceName">Source name</param>
    /// <param name="dictionaryProvider">Extension dictionaries</param>
    public LocalizationSourceExtensionInfo(
        string sourceName,
        ILocalizationDictionaryProvider dictionaryProvider)
    {
        this.SourceName = sourceName;
        this.DictionaryProvider = dictionaryProvider;
    }
}