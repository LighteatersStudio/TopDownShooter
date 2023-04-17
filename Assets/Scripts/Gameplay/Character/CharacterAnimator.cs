using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int SpeedName = Animator.StringToHash("Speed");
        private static readonly int HitName = Animator.StringToHash("Hit");
        
        private Animator _animator;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayHitAnimation()
        {
            _animator.SetTrigger(HitName);
        }
    }
}