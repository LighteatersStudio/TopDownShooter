using System;
using Gameplay.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const int LerpSpeed = 6;
        private const float RotationLerpSpeed = 2.2f;
        private const float BaseSpeed = 8.8f;
        private const float TurnValue = 0.6f;

        private readonly PlayerAnimatorParams _params = new ();
        
        [SerializeField] private GameObject _view;

        private CharacterColorFeedback.Factory _colorFeedbackFactory;
        private Animator _animator;
        private ICharacter _character;
        private CharacterColorFeedback _colorFeedback;
        private IInputController _inputController;
        private IPlayerSettings _settings;
        private Vector2 _previousLookDirection;
        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        private Vector2 _lookDirection = Vector2.up;
        private float _currentSpeed;
        private float _rotationValue;
        private bool _isRotating;

        public event Action<Vector2> OnDirectionChanged;
        
        [Inject]
        public void Construct(ICharacter character,
            CharacterColorFeedback.Factory colorFeedbackFactory,
            IInputController inputController,
            IPlayerSettings settings)
        {
            _character = character;
            _colorFeedbackFactory = colorFeedbackFactory;
            _inputController = inputController;
            _settings = settings;
        }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            _animator.SetFloat(_params.MoveSpeed, 1);
            _animator.SetFloat(_params.Turn, 0);

            _previousLookDirection = _lookDirection;
            _colorFeedback = _colorFeedbackFactory.Create(_view);
            transform.SetZeroPositionRotation();
            Subscribe();
        }

        protected void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _character.Damaged += OnDamaged;
            _character.Dead += OnDead;

            _inputController.MoveChanged += SetMoveDirection;
            _inputController.LookChanged += SetLookDirection;
        }
        
        private void Unsubscribe()
        {
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;

            _inputController.MoveChanged -= SetMoveDirection;
            _inputController.LookChanged -= SetLookDirection;
        }

        protected void Update()
        {
            DirectionAnimation();
            RotationAnimation();
        }

        private float CalculateSpeed()
        {
            const float minAnimSpeed = 0.5f;
            const float maxAnimSpeed = 2;

            var animationSpeed = _settings.Stats.MoveSpeed / BaseSpeed;
            animationSpeed = Mathf.Clamp(animationSpeed, minAnimSpeed, maxAnimSpeed);

            _currentSpeed = Mathf.Clamp(animationSpeed, minAnimSpeed, maxAnimSpeed);

            return _currentSpeed;
        }

        private void RotationAnimation()
        {
            var direction = _previousLookDirection.x * _lookDirection.y - _previousLookDirection.y * _lookDirection.x;
            
            var turnValue = TurnValue;
            
            switch (direction)
            {
                case > 0:
                    turnValue *= 1;
                    break;
                case < 0:
                    turnValue *= -1;
                    break;
            }
            
            _isRotating = _lookDirection != _previousLookDirection;
            _previousLookDirection = _lookDirection;
            

            var target = _isRotating ? turnValue : 0;
            _rotationValue = Mathf.MoveTowards(_rotationValue, target, Time.deltaTime * RotationLerpSpeed);
            _animator.SetFloat(_params.Turn, _rotationValue);
        }

        private void DirectionAnimation()
        {
            var correctedDirection = ConvertToLocal(_targetDirection);
            
            OnDirectionChanged?.Invoke(correctedDirection);
            _currentDirection = Vector2.MoveTowards(_currentDirection,
                correctedDirection, Time.deltaTime * LerpSpeed);
            
            _animator.SetFloat(_params.Horizontal, _currentDirection.x);
            _animator.SetFloat(_params.Vertical, _currentDirection.y);
        }

        private Vector2 ConvertToLocal(Vector2 worldDirection)
        {
            var right = new Vector2(_lookDirection.y, -_lookDirection.x);
            var forward = _lookDirection;

            var localX = Vector2.Dot(worldDirection, right);
            var localY = Vector2.Dot(worldDirection, forward);

            localX = NormalizeDirection(localX);
            localY = NormalizeDirection(localY);

            return new Vector2(localX, localY);
        }

        private float NormalizeDirection(float value)
        {
            return Mathf.Round(value);
        }

        private void SetMoveDirection(Vector2 direction)
        {
            _targetDirection = new Vector2(NormalizeDirection(direction.x), NormalizeDirection(direction.y));
            
            _animator.SetFloat(_params.MoveSpeed, direction.magnitude > 0 ? CalculateSpeed() : 1);
        }

        private void SetLookDirection(Vector2 direction)
        {
            _lookDirection = direction.normalized;
        }

        private void OnDead()
        {
            _animator.SetTrigger(_params.Dead);
        }

        private void OnDamaged()
        {
            _colorFeedback.ChangeColor();
            _animator.SetTrigger(_params.Hit);
        }
    }
}