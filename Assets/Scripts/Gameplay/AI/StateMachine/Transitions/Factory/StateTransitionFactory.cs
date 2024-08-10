using System.Threading;
using Zenject;

namespace Gameplay.AI
{
    public class StateTransitionFactory<TTransition> : PlaceholderFactory<CancellationToken, TTransition>, IStateTransitionFactory
        where TTransition : IStateTransition
    {
        IStateTransition IStateTransitionFactory.Create(CancellationToken token)
        {
            return Create(token);
        }
    }
}