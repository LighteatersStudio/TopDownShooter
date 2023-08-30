using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class OnceEnemySpawner : EnemySpawner
    {
        [SerializeField] private WeaponSettings _weaponSettings;

        private Cooldown _currentCooldown;
        private Weapon.Factory _weaponFactory;
        
        [Inject]
        private void Construct(Weapon.Factory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }
        protected void Start()
        {
            SpawnOne();
            Destroy(gameObject);
        }
        
        private void SpawnOne()
        {
            var enemy = Spawn();
            enemy.ChangeWeapon(_weaponFactory.Create(_weaponSettings, enemy));
        }
    }
}