using System.Collections.Generic;
using System.Globalization;
using CustomMvvm.Utilities;

namespace CustomMvvm.Localization
{
    public class LocalizationProviderViewModel : NavigableViewModel
    {
        private readonly ILocalizationProvider _localizationProvider;
        private readonly ISettingsProvider _settingsProvider;
        private CultureInfo _originalCulture;
        private CultureInfo _selectedCulture;
        public List<CultureInfo> AvailableLanguages { get; }

        public CultureInfo SelectedCulture
        {
            get { return _selectedCulture; }
            set {
                if (Equals( value, _selectedCulture ))
                {
                    return;
                }
                _selectedCulture = value;
                _localizationProvider.SetLanguage( value );
                OnPropertyChanged();
            }
        }

        public LocalizationProviderViewModel( ILocalizationProvider localizationProvider, ISettingsProvider settingsProvider )
        {
            _localizationProvider = localizationProvider;
            _settingsProvider = settingsProvider;
            _selectedCulture = _localizationProvider.GetCurrentCulture();
            _originalCulture = _selectedCulture;
            AvailableLanguages = new List<CultureInfo>(localizationProvider.GetAvailableLanguages());
        }
        
        public override void OnNavigatedFrom()
        {
            SelectedCulture = _originalCulture;
            _localizationProvider.SetLanguage( _selectedCulture );
        }

        public void Save()
        {
            _settingsProvider["Culture"] = _selectedCulture.Name;
            _originalCulture = _selectedCulture;
        }
    }
}
