using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class MoveBehaviourInputDebug : MonoBehaviour
    {
        private IMovable _movable;

        [SerializeField] private bool _moveForward;
        [SerializeField] private bool _moveRight;
        [SerializeField] private bool _moveLeft;
        [SerializeField] private bool _moveBackward;
        [SerializeField] private bool _stop;
        
        [Inject]
        public void Construct(IMovable movable)
        {
            _movable = movable;
        }
        
        private void Update()
        {
            if (_movable == null)
            {
                enabled = false;
                Debug.LogWarning("Not Found IMovable component");
            }
            
            TryMove(Vector3.forward, ref _moveForward);
            TryMove(Vector3.left, ref _moveLeft);
            TryMove(Vector3.right, ref _moveRight);
            TryMove(Vector3.back, ref _moveBackward);
            TryMove(Vector3.zero, ref _stop);
        }

        private void TryMove(Vector3 direction, ref bool boolValue)
        {
            if (boolValue)
            {
                _movable.SetMoveForce(direction);
                boolValue = false;    
            }
        }
    }
}