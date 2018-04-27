using System.Globalization;
using System.Linq;
using CustomMvvm;
using CustomMvvm.Localization;
using CustomMvvm.Utilities;
using Moq;
using NUnit.Framework;

namespace CustomMvvmTest.Localization
{
    [TestFixture]
    public class WpfLocalizationProviderTest
    {
        private const string SettingsCulture = "ru";
        private const string SettingsAvailableCultures = "ru;pl;en;de";
        private WpfLocalizationProvider _localizationProvider;
        private Mock<ISettingsProvider> _settingsMock;
        private Mock<IResourceManager> _resourceMock;

        [SetUp]
        public void Initialize()
        {
            _settingsMock = new Mock<ISettingsProvider>();
            _settingsMock.Setup( x => x["Culture"] ).Returns( SettingsCulture );
            _resourceMock = new Mock<IResourceManager>();
            _localizationProvider = new WpfLocalizationProvider( _settingsMock.Object, _resourceMock.Object );
        }
        
        [Test]
        public void ByDefaultSetsCultureProvidedBySettings()
        {
            var result = CultureInfo.DefaultThreadCurrentUICulture.Name;

            Assert.AreEqual( SettingsCulture, result );
        }

        [Test]
        public void SetsProvidedCulture()
        {
            var culture = new CultureInfo("de");

            _localizationProvider.SetLanguage( culture );
            var actual = CultureInfo.DefaultThreadCurrentUICulture;

            Assert.AreEqual( culture, actual );
        }

        [Test]
        public void GetCurrentCultureProvidesMostRecentlySavedValue()
        {
            var result = _localizationProvider.GetCurrentCulture();

            Assert.AreEqual( SettingsCulture, result.Name );
        }

        [Test]
        public void GetAvailableLanguagesParsesCulturesFromSettings()
        {
            _settingsMock.Setup( x => x["AvailableCultures"] ).Returns( SettingsAvailableCultures );
            var result = _localizationProvider.GetAvailableLanguages();

            Assert.AreEqual( 4, result.Count() );
        }

        [Test]
        public void ReturnsPhrasesFromResourceManager()
        {
            var key = "This is the key";
            var expected = "And here's the response";
            _resourceMock.Setup( x => x.GetString( key ) ).Returns( expected );

            var actual = _localizationProvider[key];

            Assert.AreEqual( expected, actual );
        }
    }
}
