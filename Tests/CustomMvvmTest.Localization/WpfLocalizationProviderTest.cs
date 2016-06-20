using System.Globalization;
using System.Linq;
using CustomMvvm;
using CustomMvvm.Localization;
using CustomMvvm.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomMvvmTest.Localization
{
    [TestClass]
    public class WpfLocalizationProviderTest
    {
        private const string SettingsCulture = "ru";
        private const string SettingsAvailableCultures = "ru;pl;en;de";
        private WpfLocalizationProvider _localizationProvider;
        private Mock<ISettingsProvider> _settingsMock;
        private Mock<IResourceManager> _resourceMock;

        [TestInitialize]
        public void Initialize()
        {
            _settingsMock = new Mock<ISettingsProvider>();
            _settingsMock.Setup( x => x["Culture"] ).Returns( SettingsCulture );
            _resourceMock = new Mock<IResourceManager>();
            _localizationProvider = new WpfLocalizationProvider( _settingsMock.Object, _resourceMock.Object );
        }
        
        [TestMethod]
        public void ByDefaultSetsCultureProvidedBySettings()
        {
            var result = CultureInfo.DefaultThreadCurrentUICulture.Name;

            Assert.AreEqual( SettingsCulture, result );
        }

        [TestMethod]
        public void SetsProvidedCulture()
        {
            var culture = new CultureInfo("de");

            _localizationProvider.SetLanguage( culture );
            var actual = CultureInfo.DefaultThreadCurrentUICulture;

            Assert.AreEqual( culture, actual );
        }

        [TestMethod]
        public void GetCurrentCultureProvidesMostRecentlySavedValue()
        {
            var result = _localizationProvider.GetCurrentCulture();

            Assert.AreEqual( SettingsCulture, result.Name );
        }

        [TestMethod]
        public void GetAvailableLanguagesParsesCulturesFromSettings()
        {
            _settingsMock.Setup( x => x["AvailableCultures"] ).Returns( SettingsAvailableCultures );
            var result = _localizationProvider.GetAvailableLanguages();

            Assert.AreEqual( 4, result.Count() );
        }

        [TestMethod]
        public void ReturnsPhrasesFromResourceManager()
        {
            var key = "This is the key";
            var expected = "And here's the response";
            _resourceMock.Setup( x => x.GetString( key ) ).Returns( expected );

            var actual = _localizationProvider[key];

            Assert.AreEqual<string>( expected, actual );
        }
    }
}
