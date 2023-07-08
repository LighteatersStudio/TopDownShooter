using NUnit.Framework;
using Zenject;

namespace Meta.Tests
{
    public abstract class ZenjectUnitTestFixture
    {
        DiContainer _container;

        protected DiContainer Container => _container;

        [SetUp]
        public virtual void Setup()
        {
            _container = new DiContainer();
        }
    }
}