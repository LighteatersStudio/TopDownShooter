using UnityEngine;

namespace Gameplay.Projectile
{
    public class MovementStraight : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;


        private void Start()
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