using System.Linq;
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
            var curPoints = _path.Points.ToList();
            
            do
            {
                foreach (var pathPoint in curPoints)
                {
                    if (!await _moving.MoveTo(pathPoint))
                    {
                        Debug.LogError($"MovePoint NOT reachable: {pathPoint}");
                    }
                }   
                
                await UniTask.Yield(cancellationToken: _token);
                curPoints.Reverse();
            }
            while (_token.IsCancellationRequested);
            
            return new StateResult(_factory.Create(_token), false);
        }
       
        public class Factory : PlaceholderFactory<CancellationToken, PatrolAIState>
        {
        }
    }
}