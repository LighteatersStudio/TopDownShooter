using Meta.Level;
using NUnit.Framework;
using Services.Coloring;
using Services.Loading;

namespace Meta.Tests
{
    [TestFixture]
    public class GameRunTest : ZenjectUnitTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            Container.Bind<ILoadingService>()
                .To<ILoadingService.Fake>()
                .AsSingle();

            Container.Bind<IColorSchemeSettings>()
                .To<IColorSchemeSettings.Fake>()
                .AsSingle();

            Container.Bind<GameColoring>().AsSingle();

            Container.BindFactory<GameRunType, GameRun, GameRun.Factory>().AsSingle();
        }

        [Test]
        public void Bind()
        {
            var factory = Container.TryResolve<GameRun.Factory>();

            Assert.NotNull(factory,"Resolving from container failed. Problem with installing");
        }

        [Test]
        public void Create()
        {
            var factory = Container.Resolve<GameRun.Factory>();

            var run = factory.Create(GameRunType.High);

            Assert.NotNull(run,"Object creating fail");
        }

        [Test]
        public void RunType()
        {
            var factory = Container.Resolve<GameRun.Factory>();

            var run = factory.Create(GameRunType.High);
            Assert.IsTrue(run.RunType == GameRunType.High);

            run = factory.Create(GameRunType.Stone);
            Assert.IsTrue(run.RunType == GameRunType.Stone);
        }
    }
}