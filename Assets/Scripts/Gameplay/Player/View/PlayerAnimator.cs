using System;
using Gameplay.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private ICharacter _character;
        private IInputController _inputController;
        private Animator _animator;
        private PlayerAnimatorNames _names;
        private IPlayerSettings _settings;
        private IPlayerAnimator[] _playerAnimators;
        private CharacterColorFeedback.Factory _colorFeedbackFactory;
        private CharacterColorFeedback _colorFeedback;

        public event Action<Vector2> OnDirectionChanged;

        [Inject]
        public void Construct(ICharacter character,
            IInputController inputController,
            PlayerAnimatorNames names,
            IPlayerSettings settings,
            CharacterColorFeedback.Factory colorFeedbackFactory)
        {
            _character = character;
            _inputController = inputController;
            _names = names;
            _settings = settings;
            _colorFeedbackFactory = colorFeedbackFactory;
        }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            var rotationAnimator = new RotationAnimator(_animator, _inputController, _names);
            var directionalAnimator = new DirectionalAnimator(_animator, _inputController, _names, rotationAnimator,
                direction => OnDirectionChanged?.Invoke(direction), _settings.Stats.MoveSpeed);

            _playerAnimators = new IPlayerAnimator[]
            {
                rotationAnimator,
                directionalAnimator
            };
        }

        protected void Start()
        {
            _animator.SetFloat(_names.MoveSpeed, 1);
            _animator.SetFloat(_names.Turn, 0);

            _colorFeedback = _colorFeedbackFactory.Create(_view);
            transform.SetZeroPositionRotation();

            Subscribe();
            
            foreach (var anim in _playerAnimators)
            {
                anim.Initialize();
            }
        }

        protected void OnDestroy()
        {
            Unsubscribe();
            
            foreach (var anim in _playerAnimators)
            {
                anim.Dispose();
            }
        }

        private void Subscribe()
        {
            _character.Damaged += OnDamaged;
            _character.Dead += OnDead;
        }

        private void Unsubscribe()
        {
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;
        }

        protected void Update()
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var anim in _playerAnimators)
            {
                anim.Update(deltaTime);
            }
        }

        private void OnDead()
        {
            _animator.SetTrigger(_names.Dead);
        }

        private void OnDamaged()
        {
            _colorFeedback.ChangeColor();
            _animator.SetTrigger(_names.Hit);
        }
    }
}