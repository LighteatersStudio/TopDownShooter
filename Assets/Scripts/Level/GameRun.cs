using System.Threading.Tasks;
using Coloring;
using Loading;
using Zenject;

namespace Level
{
    public class GameRun : IGameRun
    {
        private readonly LoadingService _loadingService;
        private readonly LevelLoadingOperation _levelLoading;
        private readonly MainMenuLoadingOperation _menuLoading;
        private readonly GameColoring _gameColoring;
        
        private readonly GameRunType _runType;
        
        public GameRun(GameRunType runType,
            LoadingService loadingService,
            LevelLoadingOperation levelLoading,
            MainMenuLoadingOperation menuLoading,
            GameColoring gameColoring)
        {
            _runType = runType;
            
            _loadingService = loadingService;
            _levelLoading = levelLoading;
            _menuLoading = menuLoading;
            _gameColoring = gameColoring;
        }

        public async Task Start()
        {
            ChoiceGameColor();

            await _loadingService.Load(_levelLoading);
        }

        public async Task Finish()
        {
            RestoreDefaultGameColor();
            await _loadingService.Load(_menuLoading);
        }
        
        private void ChoiceGameColor()
        {
            _gameColoring.SwitchTo(_runType == GameRunType.High
                ? _gameColoring.Settings.High
                : _gameColoring.Settings.Stone);
        }
        
        private void RestoreDefaultGameColor()
        {
            _gameColoring.SwitchTo(_gameColoring.Settings.Default);
        }
        
        
        public class Factory : PlaceholderFactory<GameRunType, GameRun>
        {
        }
    }
}