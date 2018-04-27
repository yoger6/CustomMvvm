using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CustomMvvm.Localization;
using CustomMvvm.Utilities;
using Moq;
using NUnit.Framework;

namespace CustomMvvmTest.Localization
{
    [TestFixture]
    public class LocalizationProviderViewModelTest
    {
        private IEnumerable<CultureInfo> AvailableLanguages
        {
            get
            {
                yield return new CultureInfo( "en" );
                yield return new CultureInfo( "de" );
                yield return new CultureInfo( "ru" );
            }
        }
        private Mock<ILocalizationProvider> _localizationMock;
        private Mock<ISettingsProvider> _settingsMock;
        private LocalizationProviderViewModel _viewModel;
        private CultureInfo _defaultCulture;

        [SetUp]
        public void Initialize()
        {
            _defaultCulture = new CultureInfo( "de" );
            _localizationMock = new Mock<ILocalizationProvider>();
            _settingsMock = new Mock<ISettingsProvider>();
            _localizationMock.Setup( x => x.GetAvailableLanguages() ).Returns( AvailableLanguages );
            _localizationMock.Setup( x => x.GetCurrentCulture() ).Returns( _defaultCulture );
            _viewModel = new LocalizationProviderViewModel( _localizationMock.Object, _settingsMock.Object );
        }

        [Test]
        public void LanguageListContainsAvailableLanguages()
        {
            Assert.AreEqual( AvailableLanguages.Count(), _viewModel.AvailableLanguages.Count );
        }

        [Test]
        public void SelectsCurrentCulture()
        {
            Assert.AreEqual( _defaultCulture, _viewModel.SelectedCulture );
        }

        [Test]
        public void WhenSelectedCultureChangesProviderPersistsThatChange()
        {
            var culture = new CultureInfo( "ru" );

            _viewModel.SelectedCulture = culture;

            _localizationMock.Verify( x => x.SetLanguage( culture ) );
        }

        [Test]
        public void WhenNavigatingFromAndCultureNotSavedRestoresOriginalCulture()
        {
            _viewModel.SelectedCulture = new CultureInfo( "en" );
            _viewModel.OnNavigatedFrom();

            Assert.AreSame( _defaultCulture, _viewModel.SelectedCulture );
            _localizationMock.Verify(x=>x.SetLanguage( _defaultCulture ));
        }

        [Test]
        public void WhenNavigatingFromAndCultureSavedPersistSelectedCulture()
        {
            var cultureInfo = _viewModel.SelectedCulture = new CultureInfo( "en" );

            _viewModel.Save();
            _viewModel.OnNavigatedFrom();

            Assert.AreSame( cultureInfo, _viewModel.SelectedCulture );
            _localizationMock.Verify( x => x.SetLanguage( cultureInfo ) );
        }
    }
}
