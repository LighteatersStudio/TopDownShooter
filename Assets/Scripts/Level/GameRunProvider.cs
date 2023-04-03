using Loading;
using Zenject;

namespace Level
{
    public class GameRunProvider
    {
        private readonly LoadingService _loadingService;
        private readonly LevelLoadingOperation _levelLoading;
        private readonly MainMenuLoadingOperation _menuLoading;

        private GameRun _gameRun;

        public IGameRun GameRun => _gameRun;
        
        [Inject]
        public GameRunProvider(LoadingService loadingService, LevelLoadingOperation levelLoading, MainMenuLoadingOperation menuLoading)
        {
            _loadingService = loadingService;
            _levelLoading = levelLoading;
            _menuLoading = menuLoading;
        }

        public async void Run(GameRunType runType)
        {
            _gameRun = new GameRun(runType, _loadingService, _levelLoading, _menuLoading);
            await _gameRun.Start();
        }
    }
}