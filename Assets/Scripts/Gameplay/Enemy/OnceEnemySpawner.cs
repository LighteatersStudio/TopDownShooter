using Gameplay.AI;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class OnceEnemySpawner : MonoBehaviour
    {
        [SerializeField] private StatsInfo _statsInfo = new() {MaxHealth = 100, Health = 100, MoveSpeed = 1, AttackSpeed = 1};
        [SerializeField] private GameObject _modelPrefab;

        [SerializeField] private SimpleEnemyAI _simpleEnemyAI;
        
        private EnemyFactory _enemyFactory;

        [Inject]
        protected void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
         
        protected void Start()
        {
            var settings = new CharacterSettings(_statsInfo,
                parent => Instantiate(_modelPrefab, parent),
                TypeGameplayObject.Enemy);
            
            var enemy = _enemyFactory.Create(settings, _simpleEnemyAI);
            
            enemy.transform.position = transform.position;
            
            Destroy(gameObject);
        }
    }
}