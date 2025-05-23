using System;
using Gameplay.Services.Input;
using UnityEngine;

namespace Gameplay.View
{
    public class DirectionalAnimator : IPlayerAnimator
    {
        private const float BaseSpeed = 8.8f;
        private const int DefaultSpeedAnimation = 1;
        private const int LerpSpeed = 6;

        private readonly Animator _animator;
        private readonly IInputController _inputController;
        private readonly PlayerAnimatorNames _names;
        private readonly RotationAnimator _rotationAnimator;
        private readonly Action<Vector2> _directionChangedHandler;
        private readonly float _moveSpeed;

        private Vector2 _currentDirection;
        private Vector2 _targetDirection;

        public DirectionalAnimator(Animator animator,
            IInputController inputController,
            PlayerAnimatorNames names,
            RotationAnimator rotationAnimator,
            Action<Vector2> directionChangedHandler,
            float moveSpeed)
        {
            _animator = animator;
            _inputController = inputController;
            _names = names;
            _rotationAnimator = rotationAnimator;
            _directionChangedHandler = directionChangedHandler;
            _moveSpeed = moveSpeed;
        }

        public void Initialize()
        {
            _inputController.MoveChanged += SetMoveDirection;
        }

        public void Dispose()
        {
            _inputController.MoveChanged -= SetMoveDirection;
        }

        public void Update(float deltaTime)
        {
            DirectionAnimation(deltaTime);
        }

        private void DirectionAnimation(float deltaTime)
        {
            var correctedDirection = ConvertToLocal(_targetDirection);

            _directionChangedHandler?.Invoke(correctedDirection);

            _currentDirection = Vector2.MoveTowards(_currentDirection,
                correctedDirection, deltaTime * LerpSpeed);

            _animator.SetFloat(_names.Horizontal, _currentDirection.x);
            _animator.SetFloat(_names.Vertical, _currentDirection.y);
        }

        private Vector2 ConvertToLocal(Vector2 worldDirection)
        {
            var right = new Vector2(_rotationAnimator.LookDirection.y, -_rotationAnimator.LookDirection.x);
            var forward = _rotationAnimator.LookDirection;

            var localX = Mathf.Round(Vector2.Dot(worldDirection, right));
            var localY = Mathf.Round(Vector2.Dot(worldDirection, forward));

            return new Vector2(localX, localY);
        }

        private void SetMoveDirection(Vector2 direction)
        {
            _targetDirection = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));

            _animator.SetFloat(_names.MoveSpeed, direction.magnitude == 0 ? DefaultSpeedAnimation : CalculateSpeed());
        }

        private float CalculateSpeed()
        {
            const float minAnimSpeed = 0.5f;
            const float maxAnimSpeed = 2;

            var animationSpeed = _moveSpeed / BaseSpeed;

            return Mathf.Clamp(animationSpeed, minAnimSpeed, maxAnimSpeed);
        }
    }
}