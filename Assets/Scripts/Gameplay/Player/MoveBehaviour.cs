using UnityEngine;

namespace Gameplay
{
    public sealed class MoveBehaviour : MonoBehaviour, IMovable
    {
        private Rigidbody _rigidbody;
        private readonly float _speed = 10;
        
        private void Start()
        {
            InitRigidbody();
            AddCollider();
        }
        
        private void InitRigidbody()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            if (_rigidbody == null)
            {
                _rigidbody = gameObject.AddComponent<Rigidbody>();
            }

            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY
                                     | RigidbodyConstraints.FreezeRotationX
                                     | RigidbodyConstraints.FreezeRotationZ;
        }

        private void AddCollider()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.height = 1.8f;
            capsuleCollider.radius = 0.5f;   
        }

        public void SetMoveForce(Vector3 direction, float force = 1)
        {
            _rigidbody.velocity = direction.normalized * _speed * force;
        }
    }
}