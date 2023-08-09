using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    public class WeaponPlaceholder: MonoBehaviour
    {
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