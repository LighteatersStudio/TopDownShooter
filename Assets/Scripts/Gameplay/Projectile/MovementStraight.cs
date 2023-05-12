using UnityEngine;

namespace Gameplay.Projectile
{
    public class MovementStraight : MovementBase, IProjectileMovement
    {
        private float _speed;
        private Rigidbody _rigidbody;

        
        public void Move(int range, float speed)
        {
            _speed = speed;
            
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
    }
}