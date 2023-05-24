using System;

namespace Gameplay
{
    public interface IGameState
    {
        event Action Wined;
        event Action PlayerDead;
        
        void Win();
        void Death();
    }
}