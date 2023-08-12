using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    public class WeaponCollectable : MonoBehaviour
    {
        private WeaponSettings _weapon;


        [Inject]
        public void Construct(Vector3 newPosition, WeaponSettings weaponSettings)
        {
            transform.position = newPosition;
            _weapon = weaponSettings;
        }

        private void Start()
        {
            Instantiate(_weapon.ViewPrefab, transform.position, Quaternion.identity, transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                other.GetComponent<Player>().ChangeWeapon(_weapon);
                Destroy(gameObject);
            }
        }

        public class Factory : PlaceholderFactory<Vector3, WeaponSettings, WeaponCollectable>
        {
        }
    }
}