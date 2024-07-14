using System.Threading.Tasks;
using Infrastructure.Loading;
using Services.Coloring;
using Services.Loading;
using Zenject;

namespace Meta.Level
{
    public class GameRun : IGameRun
    {
        private readonly GameRunParameters _gameRunParameters;
        private readonly ILoadingService _loadingService;
        private readonly GameColoring _gameColoring;
        private readonly MainMenuLoadingOperation.Factory _mainMenuLoadingFactory;
        private readonly ArenaLoadingOperation.Factory _arenaLoadingOperationFactory;
        private readonly GameRunShopLoadingOperation.Factory _shopLoadingOperationFactory;
        private readonly GameRunContext _gameRunContext;

        public GameRunType RunType { get; }

        public GameRun(GameRunParameters gameRunParameters,
            ILoadingService loadingService,
            GameColoring gameColoring,
            MainMenuLoadingOperation.Factory mainMenuLoadingFactory,
            ArenaLoadingOperation.Factory arenaLoadingOperationFactory,
            GameRunShopLoadingOperation.Factory shopLoadingOperationFactory)
        {
            RunType = gameRunParameters.RunType;
            _gameRunContext = new GameRunContext(gameRunParameters.CharacterIndex, gameRunParameters.MaxLevel);

            _loadingService = loadingService;
            _gameColoring = gameColoring;
            _mainMenuLoadingFactory = mainMenuLoadingFactory;
            _arenaLoadingOperationFactory = arenaLoadingOperationFactory;
            _shopLoadingOperationFactory = shopLoadingOperationFactory;
        }

        public async Task Start()
        {
            ChoiceGameColor();
            await _loadingService.Load(_arenaLoadingOperationFactory.Create());
        }

        public async Task NextLevel()
        {
            if (_gameRunContext.ToNextArena())
            {
                await Finish();
                return;
            }

            await _loadingService.Load(_shopLoadingOperationFactory.Create());
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


        public class Factory : PlaceholderFactory<GameRunParameters, GameRun>
        {
        }
    }
}