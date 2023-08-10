using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class InitAIState : IAIState
    {
        private readonly NavMeshMoving _moving;
        private readonly IdleAIState.Factory _idleFactory;
        private readonly CancellationToken _token;

        public InitAIState(NavMeshMoving moving, IdleAIState.Factory idleFactory, CancellationToken token)
        {
            _moving = moving;
            _idleFactory = idleFactory;
            _token = token;
        }
        
        public Task<StateResult> Launch()
        {
            if (!_moving.Setup())
            {
                return Task.FromResult(new StateResult(new ErrorState(_token), false));
            }
            
            return Task.FromResult(new StateResult(_idleFactory.Create(_token), true));
        }

        public class Factory : PlaceholderFactory<CancellationToken, InitAIState>
        {
        }
    }
}