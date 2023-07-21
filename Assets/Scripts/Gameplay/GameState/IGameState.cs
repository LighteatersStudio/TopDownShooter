using System;

namespace Gameplay
{
    public interface IGameState
    {
        event Action Won;
        event Action PlayerDead;
        
        void Win();
        void Death();
    }
}