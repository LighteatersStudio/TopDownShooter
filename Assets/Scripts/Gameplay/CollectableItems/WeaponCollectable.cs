using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    public class WeaponCollectable : MonoBehaviour
    {
        [SerializeField] private WeaponSettings _weapon;

        private IPlayer _player;


        [Inject]
        public void Construct(IPlayer player, Vector3 newPosition)
        {
            _player = player;
            transform.position = newPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _player.ChangeWeapon(_weapon);
                Destroy(gameObject);
            }
        }

        public class Factory : PlaceholderFactory<Vector3, WeaponCollectable>
        {
        }
    }
}