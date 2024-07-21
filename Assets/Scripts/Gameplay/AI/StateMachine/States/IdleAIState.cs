using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class IdleAIState : StateBase
    {
        private readonly CancellationToken _token;
       // private readonly PatrolAIState.Factory _patrolFactory;

        public IdleAIState(CancellationToken token, /*PatrolAIState.Factory patrolFactory,*/ IdleTransitions transitions)
            : base(token, transitions.Create())
        {
            _token = token;
           // _patrolFactory = patrolFactory;
        }

        protected override void BeginInternal()
        {
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            /*return _patrolFactory.Create(_token);*/
            await Task.Delay(100000);
            throw new NotImplementedException();
        }

        protected override void EndInternal()
        {
        }

        public class Factory : PlaceholderFactory<CancellationToken, IdleAIState>
        {
        }
    }
}