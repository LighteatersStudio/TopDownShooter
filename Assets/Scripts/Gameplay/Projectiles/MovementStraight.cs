using UnityEngine;

namespace Gameplay.Projectiles
{
    public class MovementStraight : MonoBehaviour, IProjectileMovement
    {
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;


        public void Move(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            transform.forward = direction;
            
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
    }
}