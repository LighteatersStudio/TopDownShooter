using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Gameplay.AI
{
    public class IdleAIState : StateBase
    {
        private readonly CancellationToken _token;
        private readonly Factory _idleFactory;
        private readonly PatrolAIState.Factory _patrolFactory;

        public IdleAIState(CancellationToken token, Factory idleFactory, PatrolAIState.Factory patrolFactory)
            : base(token, Array.Empty<IStateTransition>())
        {
            _token = token;
            _idleFactory = idleFactory;
            _patrolFactory = patrolFactory;
        }

        protected override void BeginInternal()
        {
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            return _patrolFactory.Create(_token);
            
            while (!_token.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: _token);
            }

            return _idleFactory.Create(_token);
        }


        protected override void EndInternal()
        {
        }

        public class Factory : AIStateFactory<IdleAIState>
        {
        }
    }
}