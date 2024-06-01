using Gameplay;
using UI;

namespace Infrastructure.UI
{
    public class PlayerDeathObserver
    {
        private DeathMenu.Factory _deathMenuFactory;
        private IPlayer _player;
        private IGameState _gameState;

        public PlayerDeathObserver(DeathMenu.Factory deathMenuFactory,
            IPlayer player,
            IGameState gameState)
        {
            _deathMenuFactory = deathMenuFactory;
            _player = player;
            _gameState = gameState;

            _player.Dead += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _deathMenuFactory.Open();
            _gameState.Death();
        }
    }
}