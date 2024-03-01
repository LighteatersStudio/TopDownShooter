using System.Threading.Tasks;
using Services.Coloring;
using Services.Loading;
using Zenject;

namespace Meta.Level
{
    public class GameRun : IGameRun
    {
        private readonly ILoadingService _loadingService;
        private readonly ILevelsNavigation _levelsNavigation;
        private readonly GameColoring _gameColoring;
        private readonly GameRunContext _gameRunContext = new();

        public GameRunType RunType { get; }

        public GameRun(GameRunType runType,
            ILoadingService loadingService,
            ILevelsNavigation levelsNavigation,
            GameColoring gameColoring)
        {
            RunType = runType;

            _loadingService = loadingService;
            _levelsNavigation = levelsNavigation;
            _gameColoring = gameColoring;
        }

        public async Task Start()
        {
            ChoiceGameColor();

            await _loadingService.Load(_levelsNavigation.LevelLoading);
        }

        public async Task Finish()
        {
            RestoreDefaultGameColor();
            await _loadingService.Load(_levelsNavigation.MainMenuLoading);
        }

        private void ChoiceGameColor()
        {
            _gameColoring.SwitchTo(RunType == GameRunType.High
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