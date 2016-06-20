using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using CustomMvvm.Utilities;

namespace CustomMvvm.Localization
{
    public class WpfLocalizationProvider : ObservableObject, ILocalizationProvider
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IResourceManager _resourceManager;

        [IndexerName( "Phrase" )]
        public string this[ string key ] => GetTranslation( key );
        
        public WpfLocalizationProvider(ISettingsProvider settingsProvider, IResourceManager resourceManager)
        {
            _settingsProvider = settingsProvider;
            _resourceManager = resourceManager;
            SetLanguage( new CultureInfo( (string)_settingsProvider["Culture"] ) );
        }
       
        public void SetLanguage( CultureInfo culture )
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            OnPropertyChanged( "Phrase" );
        }

        public IEnumerable<CultureInfo> GetAvailableLanguages()
        {
            var cultures = ((string)_settingsProvider["AvailableCultures"]).Split( ';' );

            return cultures.Select( culture => new CultureInfo( culture ) );
        }

        public CultureInfo GetCurrentCulture()
        {
            return new CultureInfo( (string)_settingsProvider["Culture"] );
        }

        private string GetTranslation( string key )
        {
            return _resourceManager.GetString( key );
        }
    }
}
