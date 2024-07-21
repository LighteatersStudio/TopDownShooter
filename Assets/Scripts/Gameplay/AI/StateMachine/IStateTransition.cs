using System;

namespace Gameplay.AI
{
    public interface IStateTransition
    {
        event Action<IAIState> Activated;
    }
}