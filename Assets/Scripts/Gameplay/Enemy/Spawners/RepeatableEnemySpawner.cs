using System;
using System.Collections.Generic;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class RepeatableEnemySpawner : EnemySpawner, ITicker
    {
        [SerializeField] private float _spawnDelayS = 5f;
        [SerializeField] private int _spawnCount = 5;
        [SerializeField] private bool _firstSpawnOnStart = true;
        [SerializeField] private WeaponSettings _weaponSettings;
        
        private readonly List<Character> _spawnedEnemies = new();
        private Cooldown.Factory _cooldownFactory;
        private Cooldown _currentCooldown;
        private Weapon.Factory _weaponFactory;
        public event Action<float> Tick;
        
        
        [Inject]
        private void Construct(Cooldown.Factory cooldownFactory, Weapon.Factory weaponFactory)
        {
            _cooldownFactory = cooldownFactory;
            _weaponFactory = weaponFactory;
        }

        protected void Start()
        {
            _currentCooldown = _cooldownFactory.CreateFinished();
            
            if (_firstSpawnOnStart && CanSpawned())
            {
                SpawnOne();
            }
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);

            if (_currentCooldown.IsFinish && CanSpawned())
            {
                _currentCooldown = _cooldownFactory.Create(_spawnDelayS, this, SpawnOne);
                _currentCooldown.Launch();
            }
        }
        
        private bool CanSpawned()
        {
            return _spawnedEnemies.Count < _spawnCount;
        }
        
        private void SpawnOne()
        {
            var enemy = Spawn();
            _spawnedEnemies.Add(enemy);
            enemy.Dead += OnDead; 
            enemy.ChangeWeapon(_weaponFactory.Create(_weaponSettings, enemy));

            void OnDead()
            {
                enemy.Dead -= OnDead;
                _spawnedEnemies.Remove(enemy);
            }
        }
    }
}