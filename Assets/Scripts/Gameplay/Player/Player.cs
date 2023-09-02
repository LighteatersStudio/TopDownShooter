using UnityEngine;
using System;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using Zenject;

namespace Gameplay
{
    public class Player : MonoBehaviour, IPlayer
    {
        private IPlayerSettings _settings;
        private Character _character;
        private Weapon.Factory _weaponFactory;
        
        
        public IWeaponOwner WeaponOwner => _character;
        public IHaveHealth Health => _character;

        public event Action Dead;

        [Inject]
        public void Construct(IPlayerSettings settings, Character character, Weapon.Factory weaponFactory)
        {
            _settings = settings;
            _character = character;
            _weaponFactory = weaponFactory;
        }

        private void Start()
        {
            ChangeWeapon(_settings.DefaultWeapon);
            _character.Dead += OnDead;
        }

        private void OnDestroy()
        {
            _character.Dead -= OnDead;
        }
        
        public void ChangeWeapon(IWeaponSettings settings)
        {
            _character.ChangeWeapon(_weaponFactory.Create(settings, _character));
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        private void OnDead()
        {
            Dead?.Invoke();
        }
    }
}