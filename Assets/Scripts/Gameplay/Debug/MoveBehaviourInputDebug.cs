using UnityEngine;

namespace Gameplay
{
    public sealed class MoveBehaviourInputDebug : MonoBehaviour
    {
        private IMovable _movable;

        [SerializeField] private bool _moveForward;
        [SerializeField] private bool _moveRight;
        [SerializeField] private bool _moveLeft;
        [SerializeField] private bool _moveBackward;
        private void Start()
        {
            _movable = GetComponent<IMovable>();
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