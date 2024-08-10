using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class InitAIState : SimpleBaseState
    {
        private readonly NavMeshMoving _moving;
        private readonly IdleAIState.Factory _idleFactory;
        private readonly CancellationToken _token;

        public InitAIState(NavMeshMoving moving,
            IdleAIState.Factory idleFactory,
            CancellationToken token) 
            : base(token, Array.Empty<IStateTransitionFactory>())
        {
            _moving = moving;
            _idleFactory = idleFactory;
            _token = token;
        }

        protected override Task<IAIState> LaunchInternal(CancellationToken token)
        {
            if (!_moving.Setup())
            {
                return Task.FromResult<IAIState>(new ErrorState(_token));
            }

            return Task.FromResult<IAIState>(_idleFactory.Create(_token));
        }

        public class Factory : PlaceholderFactory<CancellationToken, InitAIState>
        {
        }
    }
}