using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class AttackAIState : BaseState
    {
        private readonly Character _character;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly PursueTargetAIState.Factory _pursueTargetAIFactory;

        public AttackAIState(CancellationToken token,
            Character character,
            NavMeshMoving moving,
            ObserveArea observeArea,
            PursueTargetAIState.Factory pursueTargetAIFactory,
            DeathTransition.Factory death) 
            : base(token,new IStateTransitionFactory[] { death })
        {
            _character = character;
            _moving = moving;
            _observeArea = observeArea;
            _pursueTargetAIFactory = pursueTargetAIFactory;
        }

        protected override void BeginInternal()
        {
            _moving.Stop();
            _observeArea.ActivateAttackCollider();
        }

        private void RotateToTarget()
        {
            var lookDirection = new Vector3(
                _observeArea.TargetsTransforms.First().position.x - _character.transform.position.x, 0,
                _observeArea.TargetsTransforms.First().position.z - _character.transform.position.z);
            _character.LookDirection = lookDirection;
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            do
            {
                if (!_observeArea.HasTarget)
                {
                    break;
                }

                RotateToTarget();
                _character.Fire();
                
                await UniTask.Yield();
            }
            while (!token.IsCancellationRequested);
            
            return ActivateState(_pursueTargetAIFactory);
        }

        protected override void EndInternal()
        {
            _observeArea.DeactivateAttackCollider();
        }

        public class Factory : PlaceholderFactory<CancellationToken, AttackAIState>
        {
        }
    }
}