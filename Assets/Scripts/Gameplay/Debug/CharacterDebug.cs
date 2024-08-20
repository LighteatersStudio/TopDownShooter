using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public sealed class CharacterDebug : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] bool _takeDamage;
        
        private Character _character;
        private FriendOrFoeFactory _friendOrFoeFactory;
        
        [ShowInInspector, ReadOnly]
        private bool _isImmune;
        
        [Inject]
        public void Construct(Character character, FriendOrFoeFactory friendOrFoeFactory)
        {
            _character = character;
            _friendOrFoeFactory = friendOrFoeFactory;
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
            _character.TakeDamage(new AttackInfo(_damage, TypeDamage.Fire, _friendOrFoeFactory.CreateEnemyTeam()));
        }
        
        [Button(nameof(ChangeImmuneToggle))]
        public void ChangeImmuneToggle()
        {
            _isImmune = !_isImmune;
            _character.GiveImmune(_isImmune);
        }
    }
}