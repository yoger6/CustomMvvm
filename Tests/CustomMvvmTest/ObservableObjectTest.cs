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
            _stub.SomeProperty = "Default";

            Assert.IsFalse(_propertyHasChanged);
        }

        [Test]
        public void ChangePropertyWithDifferentValueRaisingPropertyChangedEvent()
        {
            _stub.SomeProperty = "Other than Default";

            Assert.IsTrue(_propertyHasChanged);
        }

        private class ObservableObjectStub : ObservableObject
        {
            private string _someProperty = "Default";

            public string SomeProperty
            {
                set
                {
                    if (value == _someProperty) return;
                    _someProperty = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
