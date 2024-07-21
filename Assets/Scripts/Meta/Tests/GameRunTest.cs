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

            Container.BindFactory<GameRunParameters, GameRun, GameRun.Factory>().AsSingle();
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

            var parameters = new GameRunParameters(GameRunType.High, 0, 2);
            var run = factory.Create(parameters);

            Assert.NotNull(run,"Object creating fail");
        }

        [Test]
        public void RunType()
        {
            var factory = Container.Resolve<GameRun.Factory>();

            var parametersHigh = new GameRunParameters(GameRunType.High, 0, 2);
            var run = factory.Create(parametersHigh);
            Assert.IsTrue(run.RunType == GameRunType.High);

            var parametersStone = new GameRunParameters(GameRunType.Stone, 0, 2);
            run = factory.Create(parametersStone);
            Assert.IsTrue(run.RunType == GameRunType.Stone);
        }
    }
}