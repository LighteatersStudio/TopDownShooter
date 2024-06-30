using System;
using Zenject;

namespace Gameplay
{
    public class GameStateManager : IGameState, IInitializable
    {
        private readonly IPlayer _player;

        public event Action Won;
        public event Action PlayerDead;

        public GameStateManager(IPlayer player)
        {
            _player = player;
        }

        public void Initialize()
        {
            _player.Dead += Death;
        }

        void IGameState.Win()
        {
            _player.Dead -= Death;
            Won?.Invoke();
        }

        public void Death()
        {
            _player.Dead -= Death;
            PlayerDead?.Invoke();
        }
    }
}