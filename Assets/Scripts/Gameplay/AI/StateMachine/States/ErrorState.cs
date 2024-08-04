using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.AI
{
    public class ErrorState : SimpleBaseState
    {
        private readonly TaskCompletionSource<StateResult> _taskCompletionSource;

        public ErrorState(CancellationToken token)
            : base(token, Array.Empty<IStateTransitionFactory>())
        {
            _taskCompletionSource = new TaskCompletionSource<StateResult>();
        }
        
        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            await _taskCompletionSource.Task;
            return new EmptyState();
        }
    }
}