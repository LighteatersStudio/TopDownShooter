using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class CharacterDebug : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] bool _takeDamage;
        
        private Character _character;

        
        [Inject]
        public void Construct(Character character)
        {
            _character = character;
        }

        private void Update()
        {
            if (_takeDamage)
            {
                _takeDamage = false;
                TakeDamage();
            }
        }

        private void TakeDamage()
        {
            _character.TakeDamage(new AttackInfo(_damage, TypeDamage.Fire));
        }
    }
}