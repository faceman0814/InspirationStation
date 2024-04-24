﻿namespace FaceMan.Utils.Localization;

public interface ILocalizationManager
{
    /// <summary>Gets a localization source with name.</summary>
    /// <param name="name">Unique name of the localization source</param>
    /// <returns>The localization source</returns>
    ILocalizationSource GetSource(string name);

    /// <summary>Gets all registered localization sources.</summary>
    /// <returns>List of sources</returns>
    IReadOnlyList<ILocalizationSource> GetAllSources();
}