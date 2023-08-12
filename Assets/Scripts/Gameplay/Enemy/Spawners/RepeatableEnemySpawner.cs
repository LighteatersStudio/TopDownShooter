using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class RepeatableEnemySpawner : EnemySpawner
    {
        [SerializeField] private float _spawnDelayS = 5f;
        [SerializeField] private int _spawnCount = 5;
        
        private readonly List<Character> _spawnedEnemies = new();

        private bool _isInitialized;
        
        protected void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            if (_spawnCount > 0)
            {
                SpawnOne();
            }

            while (true)
            {
                while (_spawnedEnemies.Count >= _spawnCount)
                {
                    yield return 0;    
                }
                
                yield return new WaitForSeconds(_spawnDelayS);
                SpawnOne();
            }
        }


        private void SpawnOne()
        {
            var enemy = Spawn();
            _spawnedEnemies.Add(enemy);
            enemy.Dead += OnDead; 

            void OnDead()
            {
                enemy.Dead -= OnDead;
                _spawnedEnemies.Remove(enemy);
            }
        }
    }
}