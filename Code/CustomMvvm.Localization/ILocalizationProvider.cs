using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CustomMvvm.Localization
{
    public interface ILocalizationProvider
    {
        [IndexerName( "Phrase" )]
        string this[string key] { get; }

        void SetLanguage( CultureInfo language );
        IEnumerable<CultureInfo> GetAvailableLanguages();
        CultureInfo GetCurrentCulture();
    }
}