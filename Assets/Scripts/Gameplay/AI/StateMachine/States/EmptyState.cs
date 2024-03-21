using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.AI
{
    public class EmptyState : IAIState
    {
        private readonly TaskCompletionSource<StateResult> _taskCompletionSource;

        public EmptyState(CancellationToken token)
        {
            _taskCompletionSource = new TaskCompletionSource<StateResult>();
            token.Register(() => { _taskCompletionSource.TrySetCanceled(); });
        }

        public void Begin()
        {
        }

        public async Task<IAIState> Launch()
        {
            await _taskCompletionSource.Task;
            
            return new EmptyState(CancellationToken.None);
        }

        public void Release()
        {
        }
    }
}