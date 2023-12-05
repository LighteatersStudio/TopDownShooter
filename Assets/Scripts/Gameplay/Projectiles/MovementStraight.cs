using UnityEngine;

namespace Gameplay.Projectiles
{
    public class MovementStraight : MonoBehaviour, IProjectileMovement
    {
        [SerializeField] private float _speed;
        
        private Rigidbody _rigidbody;
        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        public void Move(FlyInfo flyInfo)
        {
            _cachedTransform.position = flyInfo.Position;
            _cachedTransform.forward = flyInfo.Direction;
            
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Update()
        {
            _cachedTransform.position += _cachedTransform.forward * Time.deltaTime * _speed;
        }
    }
}