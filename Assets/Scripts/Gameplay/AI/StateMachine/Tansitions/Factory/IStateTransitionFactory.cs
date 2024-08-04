using System.Threading;

namespace Gameplay.AI
{
    public interface IStateTransitionFactory
    {
        IStateTransition CreateState(CancellationToken token);
    }
}