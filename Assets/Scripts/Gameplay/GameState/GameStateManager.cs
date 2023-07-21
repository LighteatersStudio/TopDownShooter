using System;

namespace Gameplay
{
    public class GameStateManager : IGameState
    {
        public event Action Won;
        public event Action PlayerDead;
        

        void IGameState.Win()
        {
            Won?.Invoke();
        }

        public void Death()
        {
            PlayerDead?.Invoke();
        }
    }
}