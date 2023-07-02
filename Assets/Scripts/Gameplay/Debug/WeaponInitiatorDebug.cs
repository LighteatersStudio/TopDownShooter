using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class WeaponInitiatorDebug : MonoBehaviour
    {
        [SerializeField] private WeaponSettings _weapon;
        private IPlayer _player;
        
        
        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                _player.ChangeWeapon(_weapon);
            }
        }
    }
}