using Meta.Level;
using Services.Loading;

namespace Infrastructure.Loading
{
    public class LevelsNavigation : ILevelsNavigation
    {
        public ILoadingOperation LevelLoading => _levelLoadingFactory.Create();
        public ILoadingOperation MainMenuLoading => _mainMenuLoadingFactory.Create();

        private readonly LevelLoadingOperation.Factory _levelLoadingFactory;
        private readonly MainMenuLoadingOperation.Factory _mainMenuLoadingFactory;

        public LevelsNavigation(LevelLoadingOperation.Factory levelLoadingFactory, MainMenuLoadingOperation.Factory mainMenuLoadingFactory)
        {
            _levelLoadingFactory = levelLoadingFactory;
            _mainMenuLoadingFactory = mainMenuLoadingFactory;
        }
    }
}