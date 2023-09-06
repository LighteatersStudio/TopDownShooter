using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class PursueTargetAIState: IAIState
    {
        private readonly NavMeshMoving _moving;
        private readonly CancellationToken _token;
        private readonly IdleAIState.Factory _idleAIFactory;
        private readonly ObserveArea _observeArea;
        private readonly AttackingAIState.Factory _attackingAIFactory;
        
        public PursueTargetAIState(CancellationToken token,
            IdleAIState.Factory idleAIFactory,
            ObserveArea observeArea,
            NavMeshMoving moving,
            AttackingAIState.Factory attackingAIFactory)
        {
            _token = token;
            _idleAIFactory = idleAIFactory;
            _observeArea = observeArea;
            _moving = moving;
            _attackingAIFactory = attackingAIFactory;
        }

        public async Task<StateResult> Launch()
        {
            _observeArea.ActivateAttackCollider();

            do
            {
                if (_observeArea.HasTarget)
                {
                    return new StateResult(_attackingAIFactory.Create(_token), true);
                }

                await MoveToLastTargetPosition(_observeArea.LastTargetPosition, _token);
                await UniTask.Yield();
            }
            while (_token.IsCancellationRequested);

            _observeArea.DeactivateAttackCollider();

            return new StateResult(_idleAIFactory.Create(_token), true);
        }

        private async Task MoveToLastTargetPosition(Vector3 point, CancellationToken token)
        {
            if (!await _moving.MoveTo(point, token))
            {
                Debug.LogError($"MovePoint NOT REACHED: {point}");
            }
        }

        public class Factory : PlaceholderFactory<CancellationToken, PursueTargetAIState>
        {
        }
    }
}