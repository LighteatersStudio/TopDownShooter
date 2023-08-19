using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class PatrolAIState : IAIState
    {
        private readonly NavMeshMoving _moving;
        private readonly MovingPath _path;
        private readonly IdleAIState.Factory _factory;
        private readonly CancellationToken _token;
        private readonly ObserveArea _observeArea;
        private readonly AttackingAIState.Factory _attackingAIFactory;
        
        private Transform _targetTransform;

        public PatrolAIState(NavMeshMoving moving, MovingPath path, IdleAIState.Factory factory, CancellationToken token, 
            ObserveArea observeArea, AttackingAIState.Factory attackingAIFactory)
        {
            _moving = moving;
            _path = path;
            _factory = factory;
            _token = token;
            _observeArea = observeArea;
            _attackingAIFactory = attackingAIFactory;

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
            var path = _path;
            
            do
            {
                if (!_observeArea.TargetsTransforms.IsEmpty())
                {
                    return new StateResult(_attackingAIFactory.Create(_targetTransform, _token), true);
                }

                await MoveThroughPath(path.Points, _token);
                await UniTask.Yield();
                
                path = _path.Reverse();
            }
            while (!_token.IsCancellationRequested);
            
            return new StateResult(_factory.Create(_token), false);
        }

        private async Task MoveThroughPath(IEnumerable<Vector3> points, CancellationToken token)
        {
            foreach (var pathPoint in points)
            {
                if (token.IsCancellationRequested || !_observeArea.TargetsTransforms.IsEmpty())
                {
                    break;
                }
                    
                if (!await _moving.MoveTo(pathPoint, token) && !token.IsCancellationRequested)
                {
                    Debug.LogError($"MovePoint NOT REACHED: {pathPoint}");
                }
            }
        }
        
        public class Factory : PlaceholderFactory<CancellationToken, PatrolAIState>
        {
        }
    }
}