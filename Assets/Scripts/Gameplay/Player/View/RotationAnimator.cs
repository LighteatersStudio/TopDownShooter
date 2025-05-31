using Gameplay.Services.Input;
using UnityEngine;

namespace Gameplay.View
{
    public class RotationAnimator : IPlayerAnimator
    {
        private const float RotationLerpSpeed = 2.2f;
        private const float TurnValue = 0.6f;

        private readonly Animator _animator;
        private readonly IInputController _inputController;
        private readonly PlayerAnimatorNames _names;

        private Vector2 _lookDirection;
        private Vector2 _previousLookDirection;
        private float _rotationValue;
        private bool _isRotating;

        public Vector2 LookDirection => _lookDirection;

        public RotationAnimator(Animator animator,
            IInputController inputController,
            PlayerAnimatorNames names)
        {
            _inputController = inputController;
            _animator = animator;
            _names = names;
        }

        public void Initialize()
        {
            _previousLookDirection = Vector2.up;
            _lookDirection = Vector2.up;

            _inputController.LookChanged += SetLookDirection;
        }

        public void Dispose()
        {
            _inputController.LookChanged -= SetLookDirection;
        }

        public void Update(float deltaTime)
        {
            RotationAnimation(deltaTime);
        }

        private void RotationAnimation(float deltaTime)
        {
            var orientation = _previousLookDirection.x * _lookDirection.y - _previousLookDirection.y * _lookDirection.x;
            var turnValue = TurnValue;

            turnValue *= Mathf.Sign(orientation);

            _isRotating = _lookDirection != _previousLookDirection;
            _previousLookDirection = _lookDirection;

            var target = _isRotating ? turnValue : 0;
            _rotationValue = Mathf.MoveTowards(_rotationValue, target, deltaTime * RotationLerpSpeed);
            _animator.SetFloat(_names.Turn, _rotationValue);
        }

        private void SetLookDirection(Vector2 direction)
        {
            _lookDirection = direction.normalized;
        }
    }
}