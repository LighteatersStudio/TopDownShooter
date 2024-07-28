using System;

namespace Gameplay.AI
{
    public interface IStateTransition
    {
        event Action<IAIState> Activated;

        void Initialize();
        void Release();

    }

    public interface IStateTransitionFactory
    {
        IStateTransition CreateTransition();
    }
}