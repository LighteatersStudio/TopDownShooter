using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class IdleAIState : IAIState
    {
        private readonly CancellationToken _token;
        private readonly Factory _factory;
        private readonly PatrolAIState.Factory _patrolFactory;

        public IdleAIState(CancellationToken token, Factory factory, PatrolAIState.Factory patrolFactory)
        {
            _token = token;
            _factory = factory;
            _patrolFactory = patrolFactory;
        }
        
        public async Task<StateResult> Launch()
        {
            return new StateResult(_patrolFactory.Create(_token), true);
            
            while (!_token.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: _token);
            }

            return new StateResult(_factory.Create(_token), true);
        }

        public class Factory : PlaceholderFactory<CancellationToken, IdleAIState>
        {
        }
    }
}