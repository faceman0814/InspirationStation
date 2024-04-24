namespace FaceMan.Utils.Localization;

public interface ILocalizationDictionaryProvider
{
    ILocalizationDictionary DefaultDictionary { get; }

    IDictionary<string, ILocalizationDictionary> Dictionaries { get; }

    void Initialize(string sourceName);

    void Extend(ILocalizationDictionary dictionary);
}