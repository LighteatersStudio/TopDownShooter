using System;
using UnityEngine;

namespace Gameplay
{
    public sealed class MoveBehaviour : MonoBehaviour, IMovable
    {
        private const float ColliderHeight = 1.8f;
        private const float ColliderRadius = 0.5f;
        
        
        private Rigidbody _rigidbody;
        
        private Func<float> _getSpeedHandler;

        private float Speed => _getSpeedHandler?.Invoke() ?? 1;
        
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
            capsuleCollider.height = ColliderHeight;
            capsuleCollider.radius = ColliderRadius;
        }

        public void SetSpeedHandler(Func<float> getSpeedHandler)
        {
            _getSpeedHandler = getSpeedHandler;
        }
        
        public void SetMoveForce(Vector3 direction, float force = 1)
        {
            _rigidbody.velocity = direction.normalized * Speed * force;
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}