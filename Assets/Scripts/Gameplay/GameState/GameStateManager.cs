using System;

namespace Gameplay
{
    public class GameStateManager : IGameState
    {
        public event Action Wined;
        public event Action PlayerDead;
        

        void IGameState.Win()
        {
            Wined?.Invoke();
        }

        public void Death()
        {
            PlayerDead?.Invoke();
        }
    }
}