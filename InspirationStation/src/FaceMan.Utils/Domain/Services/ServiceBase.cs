using System.Globalization;
using FaceMan.Utils.Exception;
using FaceMan.Utils.Localization;

namespace FaceMan.Utils.Domain.Services;

public abstract class ServiceBase
{
    public ILocalizationManager LocalizationManager { get; set; }
    private ILocalizationSource _localizationSource;
    protected string LocalizationSourceName { get; set; }
    protected ILocalizationSource LocalizationSource
    {
        get
        {
            if (this.LocalizationSourceName == null)
                throw new InspirationStationException("Must set LocalizationSourceName before, in order to get LocalizationSource");
            if (this._localizationSource == null || this._localizationSource.Name != this.LocalizationSourceName)
                this._localizationSource = this.LocalizationManager.GetSource(this.LocalizationSourceName);
            return this._localizationSource;
        }
    }
    /// <summary>
    /// Gets localized string for given key name and current language.
    /// </summary>
    /// <param name="name">Key name</param>
    /// <returns>Localized string</returns>
    protected virtual string L(string name) => this.LocalizationSource.GetString(name);

    /// <summary>
    /// Gets localized string for given key name and current language with formatting strings.
    /// </summary>
    /// <param name="name">Key name</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Localized string</returns>
    protected virtual string L(string name, params object[] args)
    {
        return this.LocalizationSource.GetString(name, args);
    }

    /// <summary>
    /// Gets localized string for given key name and specified culture information.
    /// </summary>
    /// <param name="name">Key name</param>
    /// <param name="culture">culture information</param>
    /// <returns>Localized string</returns>
    protected virtual string L(string name, CultureInfo culture)
    {
        return this.LocalizationSource.GetString(name, culture);
    }

    /// <summary>
    /// Gets localized string for given key name and current language with formatting strings.
    /// </summary>
    /// <param name="name">Key name</param>
    /// <param name="culture">culture information</param>
    /// <param name="args">Format arguments</param>
    /// <returns>Localized string</returns>
    protected virtual string L(string name, CultureInfo culture, params object[] args)
    {
        return this.LocalizationSource.GetString(name, culture, args);
    }
}