using UnityEngine;

namespace Gameplay.View
{
    public class PlayerAnimatorParams
    {
        public int MoveSpeed { get; private set; } = Animator.StringToHash("MoveSpeed");
        public int Horizontal { get; private set; } = Animator.StringToHash("Horizontal");
        public int Vertical { get; private set; } = Animator.StringToHash("Vertical");
        public int Turn { get; private set; } = Animator.StringToHash("Turn");
        public int Hit { get; private set; } = Animator.StringToHash("Hit");
        public int Attack { get; private set; } = Animator.StringToHash("Attack");
        public int Dead { get; private set; } = Animator.StringToHash("Dead");
        public int Reload { get; private set; } = Animator.StringToHash("Reload");
    }
}