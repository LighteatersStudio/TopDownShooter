using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.AI
{
    public class ErrorState : IAIState
    {
        private readonly TaskCompletionSource<StateResult> _taskCompletionSource;
        
        public ErrorState(CancellationToken token)
        {
            _taskCompletionSource = new TaskCompletionSource<StateResult>();
            
            token.Register(() =>
            {
                _taskCompletionSource.TrySetCanceled();
            });
        }
        
        public Task<StateResult> Launch()
        {
            return _taskCompletionSource.Task;
        }
    }
}