using System.Threading.Tasks;
using Loading;

namespace Level
{
    public class GameRun : IGameRun
    {
        private readonly LoadingService _loadingService;
        private readonly LevelLoadingOperation _levelLoading;
        private readonly MainMenuLoadingOperation _menuLoading;
        
        private readonly GameRunType _runType;
        
        public GameRun(GameRunType runType,
            LoadingService loadingService,
            LevelLoadingOperation levelLoading,
            MainMenuLoadingOperation menuLoading)
        {
            _runType = runType;
            
            _loadingService = loadingService;
            _levelLoading = levelLoading;
            _menuLoading = menuLoading;
        }

        public async Task Start()
        {
            await _loadingService.Load(_levelLoading);
        }

        public async Task Finish()
        {
            await _loadingService.Load(_menuLoading);
        }
    }
}