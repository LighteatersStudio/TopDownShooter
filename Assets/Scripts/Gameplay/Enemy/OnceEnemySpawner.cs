using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class OnceEnemySpawner : MonoBehaviour
    {
        [SerializeField] private StatsInfo _statsInfo = new() {MaxHealth = 100, Health = 100, MoveSpeed = 1, AttackSpeed = 1};
        [SerializeField] private GameObject _modelPrefab;

        private Character.Factory _characterFactory;

        
        [Inject]
        protected void Construct(Character.Factory characterFactory)
        {
            _characterFactory = characterFactory;
        }
        
        protected void Start()
        {
            var enemy = _characterFactory.Create(_statsInfo, parent => Instantiate(_modelPrefab, parent));
            enemy.transform.position = transform.position;
            
            Destroy(gameObject);
        }
    }
}