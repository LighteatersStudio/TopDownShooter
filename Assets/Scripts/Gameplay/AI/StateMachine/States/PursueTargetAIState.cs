using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class PursueTargetAIState: BaseState
    {
        private readonly NavMeshMoving _moving;
        private readonly ObserveArea _observeArea;
        private readonly SearchTargetAIState.Factory _searchTargetFactory;

        public PursueTargetAIState(CancellationToken token,
            NavMeshMoving moving,
            ObserveArea observeArea,
            SearchTargetAIState.Factory searchTargetFactory,
            DeathTransition.Factory death,
            AttackTransition.Factory attack) 
            : base(token, new IStateTransitionFactory[] { death, attack })
        {
            _moving = moving;
            _observeArea = observeArea;
            _searchTargetFactory = searchTargetFactory;
        }

        protected override void BeginInternal()
        {
            _observeArea.ActivateAttackCollider();
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            if (!await _moving.MoveTo(_observeArea.LastTargetPosition, token) && !token.IsCancellationRequested)
            {
                Debug.LogError($"MovePoint NOT REACHED: {_observeArea.LastTargetPosition}");
            }

            return ActivateState(_searchTargetFactory);
        }

        protected override void EndInternal()
        { 
            _observeArea.DeactivateAttackCollider();
        }

        public class Factory : PlaceholderFactory<CancellationToken, PursueTargetAIState>
        {
        }
    }
}