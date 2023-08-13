using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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


        public PatrolAIState(NavMeshMoving moving, MovingPath path, IdleAIState.Factory factory, CancellationToken token)
        {
            _moving = moving;
            _path = path;
            _factory = factory;
            _token = token;
        }
        
        public async Task<StateResult> Launch()
        {
            var path = _path;
            
            do
            {
                await MoveThrowPath(path.Points, _token);
                await UniTask.Yield();
                
                path = _path.Reverse();
            }
            while (!_token.IsCancellationRequested);
            
            return new StateResult(_factory.Create(_token), false);
        }

        private async Task MoveThrowPath(IEnumerable<Vector3> points, CancellationToken token)
        {
            foreach (var pathPoint in points)
            {
                if(token.IsCancellationRequested)
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