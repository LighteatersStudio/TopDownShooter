using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class SearchTargetAIState: BaseState
    {
        private const int Angle = 50;
        private const int FullRotation = 360;
        
        private readonly TargetSearchPoint _point;
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly Character _character;
        private readonly IdleAIState.Factory _idleAIFactory;

        public SearchTargetAIState(CancellationToken token,
            TargetSearchPoint point,
            NavMeshMoving moving,
            ObserveArea observeArea,
            Character character,
            IdleAIState.Factory idleAIFactory,
            DeathTransition.Factory death,
            AttackTransition.Factory attack) 
            : base(token, new IStateTransitionFactory[] { death, attack })
        {
            _point = point;
            _moving = moving;
            _observeArea = observeArea;
            _character = character;
            _idleAIFactory = idleAIFactory;
        }

        protected override void BeginInternal()
        {
            _observeArea.StopRotation();
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            await MoveToSearchTargetPosition(_point.Point, token);
            await SearchAndRotate(token);

            return ActivateState(_idleAIFactory);
        }

        private async Task MoveToSearchTargetPosition(Vector3 point, CancellationToken token)
        {
            if (!await _moving.MoveTo(point, token) && !token.IsCancellationRequested)
            {
                Debug.LogError($"MovePoint NOT REACHED: {point}");
            }
        }

        private async Task SearchAndRotate(CancellationToken token)
        {
            float currentRotation = 0;

            while (currentRotation <= FullRotation && !token.IsCancellationRequested)
            {
                var rotationSpeed = Angle * Time.deltaTime;
                
                _character.Rotate(rotationSpeed);
                currentRotation += rotationSpeed;

                await UniTask.Yield();
            }
        }

        protected override void EndInternal()
        {
            _observeArea.StartRotation();
        }

        public class Factory : PlaceholderFactory<CancellationToken, SearchTargetAIState>
        {
        }
    }
}