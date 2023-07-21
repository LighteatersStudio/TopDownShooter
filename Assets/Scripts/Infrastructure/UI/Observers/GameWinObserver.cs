using Gameplay;
using UI;
using UI.Framework;
using Zenject;

namespace Infrastructure.UI
{
    public class GameWinObserver
    {
        private readonly IUIRoot _uiRoot;
        private readonly IGameState _gameState;
        
        [Inject]
        public GameWinObserver(IUIRoot uiRoot, IGameState gameState)
        {
            _uiRoot = uiRoot;
            _gameState = gameState;
            
            _gameState.Won += OnWin;
        }

        private void OnWin()
        {
            _gameState.Won -= OnWin;
            _uiRoot.Open<WinLevelMenu>();
        }
    }
}