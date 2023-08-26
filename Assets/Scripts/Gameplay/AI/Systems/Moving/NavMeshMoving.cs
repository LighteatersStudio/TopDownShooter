using System;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Services.GameTime;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Gameplay.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoving : MonoBehaviour, ITicker
    {
        private NavMeshAgent _agent;
        private MovingAction _currentMoving;
        
        private Character _character;
        
        public event Action<float> Tick;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        
        [Inject]
        public void Construct(Character character)
        {
            _character = character;
        }
                
        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }
        
        public bool Setup()
        {
            _agent.speed = _character.MoveSpeed;

            if (!_agent.FindClosestEdge(out var hit))
            {
                Debug.LogError("Not found NavMesh closest edge");
                return false;
            }
            
            _agent.Warp(hit.position);
            return true;
        }
        
        public Task<bool> MoveTo(Vector3 position, CancellationToken token)
        {
            Stop();
            token.Register(Stop);
            
            _currentMoving = new MovingAction(_agent, this, position);
            return _currentMoving.Launch();
        }

        public void Stop()
        {
            _currentMoving?.Break();
        }
    }
}