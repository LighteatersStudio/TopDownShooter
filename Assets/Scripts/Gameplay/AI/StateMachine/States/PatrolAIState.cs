using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class PatrolAIState : SimpleBaseState
    {
        private readonly NavMeshMoving _moving;
        private readonly MovingPath _path;
        private readonly IdleAIState.Factory _factory;

        public PatrolAIState(CancellationToken token,
            NavMeshMoving moving,
            MovingPath path,
            IdleAIState.Factory factory,
            DeathTransition.Factory death) 
            : base(token, new []{death})
        {
            _moving = moving;
            _path = path;
            _factory = factory;
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
            _path.Reverse();
            
            do
            {
                await MoveThroughPath(_path.Reverse().Points, token);
                await UniTask.Yield();
                
            } while (!token.IsCancellationRequested);

            return _factory.Create(token);
        }

        private async Task MoveThroughPath(IEnumerable<Vector3> points, CancellationToken token)
        {
            foreach (var pathPoint in points)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                if (!await _moving.MoveTo(pathPoint, token))
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