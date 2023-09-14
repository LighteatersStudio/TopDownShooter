using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
        private readonly PursueTargetAIState.Factory _pursueTargetAIFactory;
        
        public AttackingAIState(CancellationToken token, Character character,
            NavMeshMoving moving, ObserveArea observeArea, IdleAIState.Factory idleAIFactory,
            PursueTargetAIState.Factory pursueTargetAIFactory)
        {
            _token = token;
            _idleAIFactory = idleAIFactory;
            _character = character;
            _moving = moving;
            _observeArea = observeArea;
            _pursueTargetAIFactory = pursueTargetAIFactory;
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

                var lookDirection = new Vector3(
                    _observeArea.TargetsTransforms.First().position.x - _character.transform.position.x, 0,
                    _observeArea.TargetsTransforms.First().position.z - _character.transform.position.z);
                _character.LookDirection = lookDirection;
                _character.Fire();
                
                await UniTask.Yield();
            }
            while (!_token.IsCancellationRequested);
            
            _observeArea.DeactivateAttackCollider();
            
            return new StateResult(_pursueTargetAIFactory.Create(_token), true);
        }

        public class Factory : PlaceholderFactory<CancellationToken, AttackingAIState>
        {
        }
    }
}