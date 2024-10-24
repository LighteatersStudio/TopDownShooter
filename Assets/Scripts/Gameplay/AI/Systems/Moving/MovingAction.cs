using System.Threading.Tasks;
using Gameplay.Services.GameTime;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.AI
{
    public class MovingAction
    {
        private const float DistanceThreshold = 1e-3f;
        
        private readonly NavMeshAgent _agent;
        private readonly ITicker _ticker;
        private readonly Vector3 _position;
        
        private readonly TaskCompletionSource<bool> _movingProcess;

        public bool IsFinished => _movingProcess.Task.IsCompleted;
        
        public MovingAction(NavMeshAgent agent, ITicker ticker,Vector3 position)
        {
            _agent = agent;
            _ticker = ticker;
            _position = position;
            
            _movingProcess = new TaskCompletionSource<bool>();
        }
        
        public Task<bool> Launch()
        {
            _ticker.Tick += OnTick;
            
            if (!_agent || !_agent.SetDestination(_position))
            {
                Finish(false);
            }
            
            return _movingProcess.Task;
        }
        
        public void Break()
        {
            Finish(false);
        }

        private int _index; //HACK!!!

        private void OnTick(float deltaTime)
        {
            _index++;

            if (_index < 10)
            {
                return;
            }

            var dist = _agent.remainingDistance;

            if (float.IsInfinity(dist) ||
                (_agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance < DistanceThreshold))
            {
                Finish(true);
            }
        }

        private void Finish(bool result)
        {
            if (!_agent.isStopped)
            {
                _agent.ResetPath();
            }
            
            _ticker.Tick -= OnTick;
            _movingProcess.TrySetResult(result);
        }
    }
}