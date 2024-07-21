using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class InitAIState : StateBase
    {
        private readonly NavMeshMoving _moving;
        private readonly IdleAIState.Factory _idleFactory;
        private readonly CancellationToken _token;

        public InitAIState(NavMeshMoving moving,
            IdleAIState.Factory idleFactory,
            IEnumerable<IStateTransition> transitions,
            CancellationToken token) 
            : base(token, transitions)
        {
            _moving = moving;
            _idleFactory = idleFactory;
            _token = token;
        }

        protected override void BeginInternal()
        {
        }

        protected override Task<IAIState> LaunchInternal(CancellationToken token)
        {
            if (!_moving.Setup())
            {
                return Task.FromResult<IAIState>(new ErrorState(_token));
            }

            return Task.FromResult<IAIState>(_idleFactory.Create(_token));
        }

        protected override void EndInternal()
        {
        }

        public class Factory : PlaceholderFactory<CancellationToken, InitAIState>
        {
        }
    }
}