﻿using Gameplay.Weapons;
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
            _weapon.ViewFactory(transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<IPlayer>();
            
            if ( player != null)
            {
                player.ChangeWeapon(_weapon);
                Destroy(gameObject);
            }
        }

        public class Factory : PlaceholderFactory<Vector3, WeaponSettings, WeaponCollectable>
        {
        }
    }
}