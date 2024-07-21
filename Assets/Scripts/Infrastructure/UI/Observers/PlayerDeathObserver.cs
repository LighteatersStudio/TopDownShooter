using System;
using Gameplay;
using UI;
using Zenject;

namespace Infrastructure.UI
{
    public class PlayerDeathObserver : IInitializable, IDisposable
    {
        private readonly DeathMenu.Factory _deathMenuFactory;
        private readonly IGameState _gameState;

        public PlayerDeathObserver(DeathMenu.Factory deathMenuFactory, IGameState gameState)
        {
            _deathMenuFactory = deathMenuFactory;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _gameState.PlayerDead += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _deathMenuFactory.Open();
        }

        public void Dispose()
        {
            _gameState.PlayerDead -= OnPlayerDeath;
        }
    }
}