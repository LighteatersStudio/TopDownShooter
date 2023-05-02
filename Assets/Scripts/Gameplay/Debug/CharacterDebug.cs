using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public sealed class CharacterDebug : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private float _damage = 10f;
        [SerializeField] bool _takeDamage;

        private IAttackInfo _attackInfo;
        
        private void Awake()
        {
            _character = GetComponent<Character>();
        }


        private void Update()
        {
            if (_takeDamage)
            {
                _takeDamage = false;
                TakeDamage();
            }
        }

        public void TakeDamage()
        {
            _character.TakeDamage(_attackInfo);
        }
    }
}