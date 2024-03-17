using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private const string ColorProperty = "_BaseColor";

        private static readonly int SpeedName = Animator.StringToHash("MoveSpeed");
        private static readonly int HitName = Animator.StringToHash("Hit");
        private static readonly int AttackName = Animator.StringToHash("Attack");
        private static readonly int DeadName = Animator.StringToHash("Dead");

        [SerializeField] private GameObject _view;

        private List<Material> _materials = new();
        private Animator _animator;
        private float _currentSpeed;
        private Vector3 _lastPosition;
        private ICharacter _character;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Construct(ICharacter character)
        {
            _character = character;
        }

        protected void Start()
        {
            foreach (var material in _view.GetComponent<SkinnedMeshRenderer>().materials)
            {
                _materials.Add(new Material(material));
            }
            
            _view.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _materials.ToArray();

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
        }

        private void Unsubscribe()
        {
            _character.Attacked -= OnAttacked;
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;
        }

        protected void Update()
        {
            _animator.SetFloat(SpeedName, CalculateSpeed());
        }

        private float CalculateSpeed()
        {
            const float maxSpeed = 10f;
            const float decelerationInS = maxSpeed * 6;

            var position = transform.position;

            _currentSpeed += (position - _lastPosition).magnitude / Time.deltaTime - decelerationInS * Time.deltaTime;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, maxSpeed);

            _lastPosition = position;

            return _currentSpeed;
        }

        private void OnDead()
        {
            _animator.SetTrigger(DeadName);
        }

        private void OnDamaged()
        {
            const float duration = 0.1f;
            
            foreach (var material in _materials)
            {
                material.DOKill();
                ChangeColor(material, Color.red, duration);
            }

            _animator.SetTrigger(HitName);
        }

        private void OnAttacked()
        {
            _animator.SetTrigger(AttackName);
        }

        private void ChangeColor(Material material, Color targetColor, float duration)
        {
            material.DOColor(targetColor, ColorProperty, duration).OnComplete(() => ResetColor(material, duration));
        }

        private void ResetColor(Material material, float duration)
        {
            material.DOColor(Color.white, ColorProperty, duration);
        }
    }
}