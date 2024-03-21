using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.AI
{
    public abstract class StateBase : IAIState
    {
        private readonly CancellationToken _mainToken;
        private readonly CancellationTokenSource _selfTokenSource;
        
        private readonly List<IStateTransition> _transitions;
        
        private IAIState _transitionState;

        protected StateBase(CancellationToken mainToken, IEnumerable<IStateTransition> transitions)
        {
            _mainToken = mainToken;
            _transitions = transitions.ToList();
            _selfTokenSource = new CancellationTokenSource();
        }
        
        public void Begin()
        {
            _mainToken.Register(_selfTokenSource.Cancel);
            
            foreach (var transition in _transitions)
            {
                transition.Activated += OnTransitionActivated;
            }
            
            BeginInternal();
        }
        protected abstract void BeginInternal();

        public async Task<IAIState> Launch()
        {
            var commonResult = await LaunchInternal(_selfTokenSource.Token);
            return _transitionState ?? commonResult;
        }
        
        protected abstract Task<IAIState> LaunchInternal(CancellationToken token);
        

        public void Release()
        {
            foreach (var transition in _transitions)
            {
                transition.Activated -= OnTransitionActivated;
            }
            
            EndInternal();
        }
        protected abstract void EndInternal();

        private void OnTransitionActivated(IAIState nextState)
        {
            _transitionState = nextState;
            _selfTokenSource.Cancel();
        }
    }
}