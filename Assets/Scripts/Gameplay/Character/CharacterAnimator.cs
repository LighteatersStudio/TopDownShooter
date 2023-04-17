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
        
        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            _lastPosition = transform.position;
        }

        protected void Update()
        {
            var position = transform.position;
            _animator.SetFloat(SpeedName, (position - _lastPosition).magnitude / Time.deltaTime);
            _lastPosition = position;
        }

        public void PlayHitAnimation()
        {
            _animator.SetTrigger(HitName);
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(AttackName);
        }
        
        public void PlayDeadAnimation()
        {
            _animator.SetTrigger(DeadName);
        }
    }
}