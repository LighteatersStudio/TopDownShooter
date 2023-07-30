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
        public void Construct(IPlayer player)
        {
            _player = player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _player.ChangeWeapon(_weapon);
                Destroy(gameObject);
            }
        }
    }
}