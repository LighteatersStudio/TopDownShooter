using Gameplay;
using UI;
using UI.Framework;
using Zenject;

namespace Infrastructure.UI
{
    public class GameWinObserver
    {
        private readonly WinLevelMenu.Factory _winMenuFactory;
        private readonly IGameState _gameState;

        [Inject]
        public GameWinObserver(WinLevelMenu.Factory winMenuFactory, IGameState gameState)
        {
            _winMenuFactory = winMenuFactory;
            _gameState = gameState;

            _gameState.Won += OnWin;
        }

        private void OnWin()
        {
            _gameState.Won -= OnWin;
            _winMenuFactory.Open();
        }
    }
}