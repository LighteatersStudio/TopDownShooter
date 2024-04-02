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
        private readonly SearchTargetAIState.Factory _searchTargetFactory;
        private readonly ObserveArea _observeArea;
        private readonly AttackingAIState.Factory _attackingAIFactory;
        private readonly DeathAIState.Factory _deathAIFactory;
        private readonly Character _character;
        
        private CancellationTokenSource _internalSource;

        public PursueTargetAIState(CancellationToken token,
            SearchTargetAIState.Factory searchTargetFactory,
            ObserveArea observeArea,
            NavMeshMoving moving,
            AttackingAIState.Factory attackingAIFactory,
            Character character)
        {
            _token = token;
            _searchTargetFactory = searchTargetFactory;
            _observeArea = observeArea;
            _moving = moving;
            _attackingAIFactory = attackingAIFactory;
            _character = character;
        }

        public async Task<StateResult> Launch()
        {
            _observeArea.ActivateAttackCollider();
            HandleCharacterDeath();
            
            if (_character.IsDead)
            {
                return new StateResult(_deathAIFactory.Create(_token), true);
            }

            await MoveToLastTargetPosition(_observeArea.LastTargetPosition, _internalSource.Token);

            if (!_observeArea.HasTarget)
            {
                return new StateResult(_searchTargetFactory.Create(_token), true);
            }

            return new StateResult(_attackingAIFactory.Create(_token), true);
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

        private async Task MoveToLastTargetPosition(Vector3 point, CancellationToken token)
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

        public class Factory : PlaceholderFactory<CancellationToken, PursueTargetAIState>
        {
        }
    }
}