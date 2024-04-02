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
        private readonly Character _character;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly PursueTargetAIState.Factory _pursueTargetAIFactory;
        private readonly DeathAIState.Factory _deathAIFactory;
        
        private CancellationTokenSource _internalSource;

        public AttackingAIState(CancellationToken token, Character character,
            NavMeshMoving moving, ObserveArea observeArea,
            PursueTargetAIState.Factory pursueTargetAIFactory)
        {
            _token = token;
            _character = character;
            _moving = moving;
            _observeArea = observeArea;
            _pursueTargetAIFactory = pursueTargetAIFactory;
        }

        public async Task<StateResult> Launch()
        {
            _moving.Stop();
            _observeArea.ActivateAttackCollider();

            HandleCharacterDeath();
            
            do
            {
                if (_character.IsDead)
                {
                    return new StateResult(_deathAIFactory.Create(_token), true);
                }
                
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
        
        private void HandleCharacterDeath()
        {
            _internalSource = new CancellationTokenSource();

            _token.Register(() => _internalSource.Cancel());

            void HandleDead()
            {
                _internalSource.Cancel();
                _character.Dead -= HandleDead;
            }

            _character.Dead += HandleDead;
        }

        public class Factory : PlaceholderFactory<CancellationToken, AttackingAIState>
        {
        }
    }
}