using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    [RequireComponent(typeof(ItemsFactory))]
    public class WeaponSpawner: MonoBehaviour
    {
        [SerializeField] private WeaponCollectable _weapon;
        
        private ItemAbstractFactory _factory;
        private IPlayer _player;

        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            _factory = gameObject.GetComponent<ItemsFactory>();
            _factory.CreateWeapon(_weapon, _player);
            
            Destroy(gameObject);
        }
    }
}