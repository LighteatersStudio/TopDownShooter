using Gameplay.AI;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Gameplay.Enemy
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SimpleEnemySettings _enemySettings;
        
        private EnemyFactory _enemyFactory;
        private Weapon.Factory _weaponFactory;

        
        [Inject]
        protected void Construct(EnemyFactory enemyFactory, Weapon.Factory weaponFactory)
        {
            _enemyFactory = enemyFactory;
            _weaponFactory = weaponFactory;
        }


        protected Character Spawn(WeaponSettings weaponSettings)
        {
            var enemy = _enemyFactory.Create(_enemySettings, _enemySettings.SimpleEnemyAI);
            enemy.transform.position = transform.position;
            
            enemy.ChangeWeapon(_weaponFactory.Create(weaponSettings, enemy));

            return enemy;
        }
    }
}