using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public abstract class BaseState : IAIState
    {
        private readonly CancellationToken _mainToken;
        private readonly List<IStateTransitionFactory> _transitionFactory;
        
        private readonly CancellationTokenSource _selfTokenSource;
        private readonly List<IStateTransition> _transitions = new ();
        private CancellationTokenRegistration _mainTokenHandler;
        
        
        private IAIState _transitionState;

        protected BaseState(CancellationToken mainToken, IEnumerable<IStateTransitionFactory> transitionsFactories)
        {
            _mainToken = mainToken;
            _transitionFactory = transitionsFactories.ToList();
            _selfTokenSource = new CancellationTokenSource();
        }
        
        public void Begin()
        {
            _mainTokenHandler = _mainToken.Register(_selfTokenSource.Cancel);

            foreach (var factory in _transitionFactory)
            {
                var transition = factory.Create(_mainToken);
                transition.Activated += OnTransitionActivated;
                transition.Initialize();
                _transitions.Add(transition);
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
            _mainTokenHandler.Dispose();
            
            foreach (var transition in _transitions)
            {
                transition.Activated -= OnTransitionActivated;
                transition.Release();
            }
            
            EndInternal();
        }
        protected abstract void EndInternal();
        
        protected TState ActivateState<TState>(PlaceholderFactory<CancellationToken, TState> factory) where TState : IAIState
        {
            return factory.Create(_mainToken);
        }

        private void OnTransitionActivated(IAIState nextState)
        {
            _transitionState = nextState;
            _selfTokenSource.Cancel();
        }
    }
}