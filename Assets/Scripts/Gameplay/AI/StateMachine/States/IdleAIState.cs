using System.Threading;
using System.Threading.Tasks;
using Zenject;


namespace Gameplay.AI
{
    public class IdleAIState : SimpleBaseState
    {
        private readonly CancellationToken _token;
        private readonly PatrolAIState.Factory _patrolFactory;

        public IdleAIState(CancellationToken token,
            PatrolAIState.Factory patrolFactory,
            DeathTransition.Factory death)
            : base(token, new []{death})
        {
            _token = token;
            _patrolFactory = patrolFactory;
        }

        protected override Task<IAIState> LaunchInternal(CancellationToken token)
        {
            return Task.FromResult<IAIState>(_patrolFactory.Create(_token));
        }

        public class Factory : PlaceholderFactory<CancellationToken, IdleAIState>
        {
        }
    }
}