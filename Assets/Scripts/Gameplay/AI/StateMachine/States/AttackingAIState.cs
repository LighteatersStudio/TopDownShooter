using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class AttackingAIState : IAIState
    {
        private readonly CancellationToken _token;
        private readonly IdleAIState.Factory _idleAIFactory;
        private readonly Character _character;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        
        public AttackingAIState(CancellationToken token, Character character,
            NavMeshMoving moving, ObserveArea observeArea, IdleAIState.Factory idleAIFactory)
        {
            _token = token;
            _idleAIFactory = idleAIFactory;
            _character = character;
            _moving = moving;
            _observeArea = observeArea;
        }

        public async Task<StateResult> Launch()
        {
            _moving.Stop();
            _observeArea.ActivateAttackCollider();

            do
            {
                if (!_observeArea.HasTarget)
                {
                    break;
                }
                
                _character.LookDirection = _observeArea.TargetsTransforms.First().position - _character.transform.position;
                _character.Fire();
                
                await UniTask.Yield();
            }
            while (!_token.IsCancellationRequested);
            
            _observeArea.DeactivateAttackCollider();
            
            return new StateResult(_idleAIFactory.Create(_token), true);
        }

        public class Factory : PlaceholderFactory<CancellationToken, AttackingAIState>
        {
        }
    }
}