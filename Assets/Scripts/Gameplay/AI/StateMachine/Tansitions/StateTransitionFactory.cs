using System.Threading;
using Zenject;

namespace Gameplay.AI
{
    public class StateTransitionFactory<TTransition> : PlaceholderFactory</*CancellationToken,*/ TTransition>, IStateTransitionFactory
        where TTransition : IStateTransition
    {
        public IStateTransition CreateState(/*CancellationToken token*/)
        {
            return Create(/*token*/);
        }
    }
}