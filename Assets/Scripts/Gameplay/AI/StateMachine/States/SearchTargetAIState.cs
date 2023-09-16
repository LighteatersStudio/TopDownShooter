using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class SearchTargetAIState: IAIState
    {
        private const int Angle = 50;
        private const int FullRotation = 360;
        
        private readonly TargetSearchPoint _point;
        private readonly CancellationToken _token;
        private readonly IdleAIState.Factory _idleAIFactory;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly Character _character;
        private readonly AttackingAIState.Factory _attackingAIFactory;

        public SearchTargetAIState(CancellationToken token,
            IdleAIState.Factory idleAIFactory,
            TargetSearchPoint point,
            NavMeshMoving moving,
            ObserveArea observeArea,
            Character character,
            AttackingAIState.Factory attackingAIFactory)
        {
            _point = point;
            _token = token;
            _idleAIFactory = idleAIFactory;
            _moving = moving;
            _observeArea = observeArea;
            _character = character;
            _attackingAIFactory = attackingAIFactory;
        }

        public async Task<StateResult> Launch()
        {
            _observeArea.DeactivateAttackCollider();
            
            await MoveToSearchTargetPosition(_point.Point, _token);
            
            float currentRotation = 0;

            while (currentRotation <= FullRotation && !_observeArea.HasTarget)
            {
                var rotationSpeed = Angle * Time.deltaTime;
                
                _observeArea.StopRatation();
                _character.Rotate(rotationSpeed);
                currentRotation += rotationSpeed;

                await UniTask.Yield();
            }

            if (_observeArea.HasTarget)
            {
                return new StateResult(_attackingAIFactory.Create(_token), true);
            }
            
            return new StateResult(_idleAIFactory.Create(_token), true);
        }

        private async Task MoveToSearchTargetPosition(Vector3 point, CancellationToken token)
        {
            var internalToken = GetTargetChangedToken(token);

            if (!await _moving.MoveTo(point, internalToken) && !internalToken.IsCancellationRequested)
            {
                Debug.LogError($"MovePoint NOT REACHED: {point}");
            }
        }

        private CancellationToken GetTargetChangedToken(CancellationToken parentToken)
        {
            CancellationTokenSource internalSource = new CancellationTokenSource();
            parentToken.Register(() => internalSource.Cancel());

            var internalToken = internalSource.Token;

            _observeArea.TargetsChanged += OnTargetChanged;
            
            void OnTargetChanged()
            {
                if (!_observeArea.HasTarget)
                {
                    return;
                }
                
                _observeArea.TargetsChanged -= OnTargetChanged;
                internalSource.Cancel();
            }
            
            return internalToken;
        }
        
        public class Factory : PlaceholderFactory<CancellationToken, SearchTargetAIState>
        {
        }
    }
}