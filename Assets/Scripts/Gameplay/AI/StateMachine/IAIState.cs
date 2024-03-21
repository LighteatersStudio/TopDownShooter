using System;
using System.Threading.Tasks;

namespace Gameplay.AI
{
    public interface IAIState
    {
        void Begin();
        Task<IAIState> Launch();
        void Release();
    }

    public interface IStateTransition
    {
        event Action<IAIState> Activated;
    }
}