using Gameplay;
using UI;
using UI.Framework;
using Zenject;

namespace Infrastructure
{
    public class GameWinObserver
    {
        private IUIRoot _uiRoot;
        private IGameState _gameState;
        
        [Inject]
        public GameWinObserver(IUIRoot uiRoot, IGameState gameState)
        {
            _uiRoot = uiRoot;
            _gameState = gameState;
            
            _gameState.Wined += OnWined;
        }

        private void OnWined()
        {
            _gameState.Wined -= OnWined;
            _uiRoot.Open<WinLevelMenu>();
        }

        
    }
}