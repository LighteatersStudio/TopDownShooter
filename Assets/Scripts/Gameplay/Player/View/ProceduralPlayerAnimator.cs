using DG.Tweening;
using Gameplay.Services.GameTime;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.View
{
    [RequireComponent(typeof(Animator))]
    public class ProceduralPlayerAnimator : MonoBehaviour
    {
        private const int LerpSpeed = 6;
        private const int RotationLerpSpeed = 30;
        private const float ReloadTimeOffset = 0.2f;
        private const float DurationAttack = 0.05f;

        private readonly Vector3 _rightDir = new(0.036f, 1.378f, 1.108f);
        private readonly Vector3 _leftDir = new(-0.312f, 1.378f, 0.828f);
        private readonly Vector3 _rightFrontDir = new(0f, 1.324f, 1.194f);
        private readonly Vector3 _frontDir = new(0f, 1.14f, 1.124f);
        private readonly Vector3 _leftHandEndValue = new(-57, 148, -84);

        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Rig _rig;
        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Transform _leftShoulderTransform;
        [SerializeField] private Transform _referenceLeftHandTransform;

        private Animator _animator;
        private IReloaded _source;
        private ICharacter _character;
        private PlayerAnimatorNames _names;
        private Quaternion _regularRotation;
        private Vector3 _startLeftHandPos;
        private Vector3 _startLeftShoulderPos;
        private Vector3 _currentLeftHandPos;

        [Inject]
        public void Construct(ICharacter character, IReloaded source, PlayerAnimatorNames animatorNames)
        {
            _character = character;
            _source = source;
            _names = animatorNames;
        }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _source.ReloadStarted += OnReloaded;
            _character.Attacked += OnAttacked;
            _playerAnimator.OnDirectionChanged += UpdateLeftHandByDirection;

            _leftHandTransform.SetLocalPositionAndRotation(_referenceLeftHandTransform.localPosition,
                _referenceLeftHandTransform.localRotation);

            _regularRotation = _leftHandTransform.localRotation;
            _startLeftHandPos = _leftHandTransform.localPosition;
            _startLeftShoulderPos = _leftShoulderTransform.localPosition;
        }

        private void OnDestroy()
        {
            _source.ReloadStarted -= OnReloaded;
            _character.Attacked -= OnAttacked;
            _playerAnimator.OnDirectionChanged -= UpdateLeftHandByDirection;
        }

        private void LateUpdate()
        {
            var leftHandRotation = _leftHandTransform.localRotation;
            var leftHandPosition = _leftHandTransform.localPosition;
            var leftShoulderPosition = _leftShoulderTransform.localPosition;

            leftHandRotation = Quaternion.Lerp(leftHandRotation, _regularRotation, Time.deltaTime * RotationLerpSpeed);
            leftHandPosition = Vector3.Lerp(leftHandPosition, _currentLeftHandPos, Time.deltaTime * LerpSpeed / 2);
            leftShoulderPosition = Vector3.Lerp(leftShoulderPosition, _startLeftShoulderPos, Time.deltaTime * LerpSpeed);

            _leftHandTransform.localRotation = leftHandRotation;
            _leftHandTransform.localPosition = leftHandPosition;
            _leftShoulderTransform.localPosition = leftShoulderPosition;
        }

        private void UpdateLeftHandByDirection(Vector2 direction)
        {
            var targetPosition = direction switch
            {
                var dir when dir == Vector2.right => _rightDir,
                var dir when dir == Vector2.left => _leftDir,
                var dir when dir == Vector2.one => _rightFrontDir,
                var dir when dir == Vector2.up => _frontDir,
                _ => _startLeftHandPos
            };

            _leftHandTransform.localPosition = Vector3.MoveTowards(
                _leftHandTransform.localPosition,
                targetPosition,
                Time.deltaTime * LerpSpeed
            );

            _currentLeftHandPos = targetPosition;
        }

        private void OnAttacked()
        {
            const float leftHandOffsetZ = 0.12f;
            const float leftShoulderOffsetZ = 0.17f;
            const float yOffset = 0.01f;

            var randomYOffset = Random.Range(-yOffset, yOffset);
            
            _leftHandTransform.DOKill();
            _leftShoulderTransform.DOKill();

            _leftHandTransform
                .DOLocalRotate(_leftHandEndValue, DurationAttack)
                .SetEase(Ease.Linear)
                .SetLink(_leftHandTransform.gameObject);

            _leftHandTransform
                .DOLocalMove(new Vector3(_currentLeftHandPos.x, _currentLeftHandPos.y + randomYOffset,
                    _currentLeftHandPos.z - leftHandOffsetZ), DurationAttack)
                .SetEase(Ease.OutCubic)
                .SetLink(_leftHandTransform.gameObject);

            _leftShoulderTransform
                .DOLocalMove(new Vector3(_startLeftShoulderPos.x, _startLeftShoulderPos.y + randomYOffset,
                        _startLeftShoulderPos.z - leftShoulderOffsetZ), DurationAttack)
                .SetEase(Ease.OutCubic)
                .SetLink(_leftShoulderTransform.gameObject);
        }

        private void OnReloaded(ICooldown cooldown)
        {
            _animator.SetTrigger(_names.Reload);
            _rig.weight = 0;

            var animationStartTime = Mathf.Max(0, cooldown.RemainingTimeS - ReloadTimeOffset);
            DOVirtual.DelayedCall(animationStartTime, () =>
            {
                DOTween.To(() => _rig.weight, x => _rig.weight = x, 1f, ReloadTimeOffset)
                    .SetEase(Ease.OutCubic)
                    .SetLink(gameObject);
            });
        }
    }
}