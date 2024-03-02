using System.Threading.Tasks;
using Infrastructure.Loading;
using Services.Coloring;
using Services.Loading;
using Zenject;

namespace Meta.Level
{
    public class GameRun : IGameRun
    {
        private readonly ILoadingService _loadingService;
        private readonly MainMenuLoadingOperation.Factory _mainMenuLoadingFactory;
        private readonly ArenaLoadingOperation.Factory _arenaLoadingOperationFactory;
        private readonly GameColoring _gameColoring;
        private readonly GameRunContext _gameRunContext = new();

        public GameRunType RunType { get; }

        public GameRun(GameRunType runType,
            ILoadingService loadingService,
            GameColoring gameColoring,
            ArenaLoadingOperation.Factory arenaLoadingOperationFactory,
            MainMenuLoadingOperation.Factory mainMenuLoadingFactory)
        {
            RunType = runType;

            _loadingService = loadingService;
            _gameColoring = gameColoring;
            _arenaLoadingOperationFactory = arenaLoadingOperationFactory;
            _mainMenuLoadingFactory = mainMenuLoadingFactory;
        }

        public async Task Start()
        {
            ChoiceGameColor();

            await _loadingService.Load(_arenaLoadingOperationFactory.Create());
        }

        public async Task NextRandomArena()
        {
            _gameRunContext.OnNextArena();
            await _loadingService.Load(_arenaLoadingOperationFactory.Create());
        }

        public async Task Finish()
        {
            RestoreDefaultGameColor();
            await _loadingService.Load(_mainMenuLoadingFactory.Create());
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