using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ModestTree;
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
        private Transform _targetTransform;
        
        public AttackingAIState(Transform targetTransform, CancellationToken token, Character character,
            NavMeshMoving moving, ObserveArea observeArea, IdleAIState.Factory idleAIFactory)
        {
            _token = token;
            _idleAIFactory = idleAIFactory;
            _character = character;
            _targetTransform = targetTransform;
            _moving = moving;
            _observeArea = observeArea;

            _observeArea.TargetsChanged += OnTargetsChanged;
        }

        private void OnTargetsChanged()
        {
            if (!_observeArea.TargetsTransforms.IsEmpty())
            {
                _targetTransform = _observeArea.TargetsTransforms.First();
            }
        }

        public async Task<StateResult> Launch()
        {
            do
            {
                if (_observeArea.TargetsTransforms.IsEmpty())
                {
                    break;
                }
                
                StopAndShoot();
                await UniTask.Yield();
            }
            while (!_token.IsCancellationRequested);

            _observeArea.TargetsChanged -= OnTargetsChanged;

            return new StateResult(_idleAIFactory.Create(_token), true);
        }

        private void StopAndShoot()
        {
            if (_observeArea.TargetsTransforms.IsEmpty())
            {
                return;
            }
            
            _moving.Stop();
            _character.LookDirection = _targetTransform.position;
            _character.Fire();
        }

        public class Factory : PlaceholderFactory<Transform, CancellationToken, AttackingAIState>
        {
        }
    }
}