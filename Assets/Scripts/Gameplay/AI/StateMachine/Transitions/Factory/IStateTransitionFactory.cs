using System.Threading;

namespace Gameplay.AI
{
    public interface IStateTransitionFactory
    {
        IStateTransition Create(CancellationToken token);
    }
}