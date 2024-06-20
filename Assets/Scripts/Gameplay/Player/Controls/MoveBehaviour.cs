using UnityEngine;

namespace Gameplay
{
    public sealed class MoveBehaviour : IMovable
    {
        private readonly Character _character;
        private readonly Rigidbody _rigidbody;
        private float Speed => _character.MoveSpeed;

        public MoveBehaviour(Character character, Rigidbody rigidbody)
        {
            _character = character;
            _rigidbody = rigidbody;

            InitRigidbody();
        }
        
        private void InitRigidbody()
        {
            _rigidbody.freezeRotation = true;
        }

        public void SetMoveForce(Vector3 direction, float force = 1)
        {
            if (_rigidbody)
            {
                _rigidbody.velocity = direction.normalized * (Speed * force);
            }
        }
    }
}