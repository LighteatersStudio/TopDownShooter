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
        private readonly PatrolAIState.Factory _patrolAIFactory;
        private readonly IdleAIState.Factory _idleAIFactory;
        private readonly Character _character;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly Transform _targetTransform;

        private bool _enemyEscaped;

        public AttackingAIState(Transform targetTransform, CancellationToken token, Character character,
            NavMeshMoving moving,
            ObserveArea observeArea, PatrolAIState.Factory patrolAIFactory, IdleAIState.Factory idleAIFactory)
        {
            _token = token;
            _patrolAIFactory = patrolAIFactory;
            _idleAIFactory = idleAIFactory;
            _character = character;
            _targetTransform = targetTransform;
            _moving = moving;
            _observeArea = observeArea;

            _observeArea.EnemyEscaped += OnEnemyEscaped;
        }

        private void OnEnemyEscaped()
        {
            _enemyEscaped = true;
            Debug.Log("Enemy just escaped");
        }

        public async Task<StateResult> Launch()
        {
            Debug.Log("Attacking AI State: attack!");

            do
            {
                if (_enemyEscaped)
                {
                    break;
                }
                
                await StopAndShoot();
                await UniTask.Yield();
            }
            while (!_token.IsCancellationRequested);

            Debug.Log("Enemy escaped = " + _enemyEscaped);
            return new StateResult(_idleAIFactory.Create(_token), true);
        }

        private async Task StopAndShoot()
        {
            if (_enemyEscaped)
            {
                return;
            }
            
            await _moving.Stop(_token);
            _character.LookDirection = _targetTransform.position;
            _character.Fire();
        }

        public class Factory : PlaceholderFactory<Transform, CancellationToken, AttackingAIState>
        {
        }
    }
}