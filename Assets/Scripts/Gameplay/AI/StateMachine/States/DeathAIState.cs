using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class DeathAIState: IAIState
    {
        private readonly CancellationToken _token;
        private readonly IdleAIState.Factory _idleFactory;
        private readonly NavMeshMoving _moving;

        public DeathAIState(CancellationToken token, IdleAIState.Factory idleFactory, NavMeshMoving moving)
        {
            _token = token;
            _idleFactory = idleFactory;
            _moving = moving;
        }

        public async Task<StateResult> Launch()
        {
            _moving.Stop();
            
            await UniTask.Delay(TimeSpan.FromSeconds(11));
            
            return new StateResult(_idleFactory.Create(_token), true);
        }

        public class Factory : PlaceholderFactory<CancellationToken, DeathAIState>
        {
        }
    }
}