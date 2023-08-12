using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SimpleEnemySettings _enemySettings;
        
        private EnemyFactory _enemyFactory;
        
        [Inject]
        protected void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
        
        
        protected Character Spawn()
        {
            var enemy = _enemyFactory.Create(_enemySettings, _enemySettings.SimpleEnemyAI);
            
            enemy.transform.position = transform.position;

            return enemy;
        }
    }
}