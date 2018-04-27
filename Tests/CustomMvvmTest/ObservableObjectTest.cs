using System.ComponentModel;
using CustomMvvm;
using NUnit.Framework;

namespace CustomMvvmTest
{
    [TestFixture]
    public class ObservableObjectTest
    {
        private ObservableObjectStub _stub;
        private bool _propertyHasChanged;

        [SetUp]
        public void Initialize()
        {
            _stub = new ObservableObjectStub();
            _stub.PropertyChanged += (sender, args) => _propertyHasChanged = true;
            _propertyHasChanged = false;
        }

        [Test]
        public void IsAbstract()
        {
            Assert.IsTrue(typeof(ObservableObject).IsAbstract);
        }

        [Test]
        public void ImplementsINotifyPropertyChanged()
        {
            Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(ObservableObject)));
        }

        [Test]
        public void ChangePropertyWithSameValueNotRaisingPropertyChangedEvent()
        {
            _stub.SomeProperty = _stub.SomeProperty;

            Assert.IsFalse(_propertyHasChanged);
        }

        [Test]
        public void ChangePropertyWithDifferentValueRaisingPropertyChangedEvent()
        {
            _stub.SomeProperty = "Other than Default";

            Assert.IsTrue( _propertyHasChanged );
        }

        [Test]
        public void PropertyChangedIsRaisedByDefaultForExectPropertyThatHaveChanged()
        {
            const string expected = nameof(_stub.SomeProperty);

            var actual = string.Empty;
            _stub.PropertyChanged += ( sender, args ) => actual = args.PropertyName;
            _stub.SomeProperty = "Fantastic";

            Assert.AreEqual( expected, actual );
        }

        private class ObservableObjectStub : ObservableObject
        {
            private string _someField = null;

            public string SomeProperty
            {
                get => _someField;
                set => Set( ref _someField, value );
            }
        }
    }
}
