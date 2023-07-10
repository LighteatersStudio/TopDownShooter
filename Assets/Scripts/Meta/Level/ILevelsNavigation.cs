using Services.Loading;

namespace Meta.Level
{
    public interface ILevelsNavigation
    {
        ILoadingOperation LevelLoading { get; }
        ILoadingOperation MainMenuLoading { get; }

        public class Fake : ILevelsNavigation
        {
            public ILoadingOperation LevelLoading { get; } = new ILoadingOperation.Fake();
            public ILoadingOperation MainMenuLoading { get; } = new ILoadingOperation.Fake();
        }
    }
}