using Zenject;

namespace Meta.Level
{
    public class GameRunProvider
    {
        private readonly GameRun.Factory _factory;

        private GameRun _gameRun;

        public IGameRun GameRun => _gameRun;
        
        [Inject]
        public GameRunProvider(GameRun.Factory factory)
        {
            _factory = factory;
        }

        public async void Run(GameRunType runType)
        {
            _gameRun = _factory.Create(runType);
            await _gameRun.Start();
        }
    }
}