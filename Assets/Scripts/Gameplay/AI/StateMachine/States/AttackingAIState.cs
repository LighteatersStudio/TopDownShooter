using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class AttackingAIState : StateBase
    {
        private readonly CancellationToken _token;
        private readonly Character _character;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;

        public AttackingAIState(CancellationToken token,
            Character character,
            NavMeshMoving moving,
            ObserveArea observeArea,
            IStateTransition targetLostTransition)
            : base(token, new[] {targetLostTransition})
        {
            _token = token;
            _character = character;
            _moving = moving;
            _observeArea = observeArea;
        }

        protected override void BeginInternal()
        {
            _moving.Stop();
            _observeArea.ActivateAttackCollider();

        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            var characterTransform = _character.transform;

            UniTask.WaitUntil(Action, PlayerLoopTiming.Update, _token);
            
            do
            {
                var lookDirection = new Vector3(
                    _observeArea.TargetsTransforms.First().position.x - characterTransform.position.x,
                    0,
                    _observeArea.TargetsTransforms.First().position.z - characterTransform.position.z);
                
                _character.LookDirection = lookDirection;
                _character.Fire();
                
                await UniTask.Yield();
            }
            while (!_token.IsCancellationRequested);

            return new EmptyState(_token);
        }

        private bool Action()
        {
            var lookDirection = new Vector3(
                _observeArea.TargetsTransforms.First().position.x - characterTransform.position.x,
                0,
                _observeArea.TargetsTransforms.First().position.z - characterTransform.position.z);
                
            _character.LookDirection = lookDirection;
            _character.Fire();

            return true;
        }


        protected override void EndInternal()
        {
            _observeArea.DeactivateAttackCollider();
        }

        public class Factory : PlaceholderFactory<CancellationToken, AttackingAIState>
        {
        }
    }


    public class TargetLostTransition : IStateTransition
    {
        private readonly CancellationToken _cancellationToken;
        private readonly ObserveArea _observeArea;
        private readonly PursueTargetAIState.Factory _pursueTargetAIFactory;
        public event Action<IAIState> Activated;

        public TargetLostTransition(CancellationToken cancellationToken,
            ObserveArea observeArea,
            PursueTargetAIState.Factory pursueTargetAIFactory)
        {
            _cancellationToken = cancellationToken;
            _observeArea = observeArea;
            _pursueTargetAIFactory = pursueTargetAIFactory;
            _observeArea.TargetsChanged += OnTargetChanged;
        }

        private void OnTargetChanged()
        {
            if (_observeArea.HasTarget)
            {
                return;
            }
                
            Activated?.Invoke(_pursueTargetAIFactory.Create(_cancellationToken));
        }
    }
}