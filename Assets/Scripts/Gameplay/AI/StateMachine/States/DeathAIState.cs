using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class DeathAIState: SimpleBaseState
    {
        private readonly NavMeshMoving _moving;
        private readonly IAIAgentStop _agentStop;

        public DeathAIState(CancellationToken token,
            NavMeshMoving moving,
            IAIAgentStop agentStop)
            : base(token, new IStateTransitionFactory[]{})
        {
            _moving = moving;
            _agentStop = agentStop;
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            _moving.Stop();
            _agentStop.Stop();

            var tcs = new TaskCompletionSource<object>();
            token.Register(() => tcs.TrySetResult(null));
            
            await tcs.Task;
            return new EmptyState();
        }

        public class Factory : PlaceholderFactory<CancellationToken, DeathAIState>
        {
        }
    }
}