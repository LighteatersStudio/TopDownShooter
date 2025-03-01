using Gameplay.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private const int LerpSpeed = 5;
        private const float RoundingCoef = 0.5f;

        private static readonly int SpeedName = Animator.StringToHash("MoveSpeed");
        private static readonly int HorizontalName = Animator.StringToHash("Horizontal");
        private static readonly int VerticalName = Animator.StringToHash("Vertical");
        private static readonly int HitName = Animator.StringToHash("Hit");
        private static readonly int AttackName = Animator.StringToHash("Attack");
        private static readonly int DeadName = Animator.StringToHash("Dead");

        [SerializeField] private GameObject _view;

        private CharacterColorFeedback.Factory _colorFeedbackFactory;
        private Animator _animator;
        private ICharacter _character;
        private CharacterColorFeedback _colorFeedback;
        private IInputController _inputController;
        private Vector3 _lastPosition;
        private Vector2 _currentDirection;
        private Vector2 _targetDirection;
        private Vector2 _lookDirection = Vector2.up;
        private float _currentSpeed;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        [Inject]
        public void Construct(ICharacter character,
            CharacterColorFeedback.Factory colorFeedbackFactory,
            IInputController inputController)
        {
            _character = character;
            _colorFeedbackFactory = colorFeedbackFactory;
            _inputController = inputController;
        }

        protected void Start()
        {
            _colorFeedback = _colorFeedbackFactory.Create(_view);
            transform.SetZeroPositionRotation();
            _lastPosition = transform.position;
            Subscribe();
        }

        protected void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _character.Attacked += OnAttacked;
            _character.Damaged += OnDamaged;
            _character.Dead += OnDead;

            _inputController.MoveChanged += SetMoveDirection;
            _inputController.LookChanged += SetLookDirection;
        }

        private void Unsubscribe()
        {
            _character.Attacked -= OnAttacked;
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;

            _inputController.MoveChanged -= SetMoveDirection;
            _inputController.LookChanged -= SetLookDirection;
        }

        protected void Update()
        {
            _animator.SetFloat(SpeedName, CalculateSpeed());
            DirectionAnimation();
        }

        private float CalculateSpeed()
        {
            const float maxSpeed = 10f;
            const float decelerationInS = maxSpeed * 6;

            var position = transform.position;

            _currentSpeed += (position - _lastPosition).magnitude / Time.unscaledDeltaTime -
                             decelerationInS * Time.unscaledDeltaTime;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, maxSpeed);

            _lastPosition = position;

            return _currentSpeed;
        }

        private void DirectionAnimation()
        {
            var correctedDirection = ConvertToLocal(_targetDirection);
            _currentDirection = Vector2.MoveTowards(_currentDirection, correctedDirection, Time.deltaTime * LerpSpeed);

            _animator.SetFloat(HorizontalName, _currentDirection.x);
            _animator.SetFloat(VerticalName, _currentDirection.y);
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
        }

        private void SetLookDirection(Vector2 direction)
        {
            _lookDirection = direction.normalized;
        }

        private void OnDead()
        {
            _animator.SetTrigger(DeadName);
        }

        private void OnDamaged()
        {
            _colorFeedback.ChangeColor();
            _animator.SetTrigger(HitName);
        }

        private void OnAttacked()
        {
            _animator.SetTrigger(AttackName);
        }
    }
}