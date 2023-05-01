using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int SpeedName = Animator.StringToHash("MoveSpeed");
        private static readonly int HitName = Animator.StringToHash("Hit");
        private static readonly int AttackName = Animator.StringToHash("Attack");
        private static readonly int DeadName = Animator.StringToHash("Dead");
        
        private Animator _animator;

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
            transform.SetZeroPositionAndRotation();
            
            _lastPosition = transform.position;
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
        }
        private void Unsubscribe()
        {
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;
        }
        
        
        protected void Update()
        {
            var position = transform.position;
            _animator.SetFloat(SpeedName, (position - _lastPosition).magnitude / Time.deltaTime);
            _lastPosition = position;
        }

        private void OnDead()
        {
            _animator.SetTrigger(DeadName);
        }

        private void OnDamaged()
        {
            _animator.SetTrigger(HitName);
        }
    }
}