using UnityEngine;

namespace Gameplay.Projectile
{
    public class MovementStraight : MonoBehaviour, IProjectileMovement
    {
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;


        public void Move()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
    }
}