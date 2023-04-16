using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour
    {
        private static int HashSpeed = Animator.StringToHash("Speed");
        
        private CharacterStats _stats;
        public ICharacterStats Stats => _stats;


        [SerializeField] private Animator _animator; 
        
        private void Init()
        {
            _stats = new CharacterStats();       
        }
        
        public void TakeDamage(float damage)
        {
            _stats.ApplyDamage(damage);
        }
            
        protected void Update()
        {
            
            _animator.SetFloat("Health", _stats.Health);
        }
    }
    
    public interface ICharacterMovement
    {
        public float Speed { get; }
    }


    public interface ICharacterStats
    {
        public float MaxHealth { get; }
        public float Health { get; }
    }
    
    public class CharacterStats : ICharacterStats
    {
        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        
        public CharacterStats()
        {
            MaxHealth = 100f;
            Health = MaxHealth;
        }
        
        public void ApplyDamage(float damage)
        {
            Health -= damage;
        }
    }
}