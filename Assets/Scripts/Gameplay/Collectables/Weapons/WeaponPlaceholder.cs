using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    public class WeaponPlaceholder: MonoBehaviour
    {
        [SerializeField] private WeaponSettings _weapon;

        private WeaponCollectable.Factory _weaponCollectableFactory;

        [Inject]
        public void Construct(WeaponCollectable.Factory weaponCollectableFactory)
        {
            _weaponCollectableFactory = weaponCollectableFactory;
        }

        private void Start()
        {
            _weaponCollectableFactory.Create(transform.position);
            Destroy(gameObject);
        }
    }
}