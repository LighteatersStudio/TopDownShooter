using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class OnceEnemySpawner : EnemySpawner
    {
        [SerializeField] private WeaponSettings _weaponSettings;

        private Cooldown _currentCooldown;
        
        protected void Start()
        {
            Spawn(_weaponSettings);
            Destroy(gameObject);
        }
    }
}