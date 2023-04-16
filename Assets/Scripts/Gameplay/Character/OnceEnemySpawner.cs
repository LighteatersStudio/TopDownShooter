using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class OnceEnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _modelPrefab;
        [SerializeField] private StatsInfo _statsInfo;
        
        
        private CharacterFactory _characterFactory;

        [Inject]
        protected void Construct(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }
        protected void Start()
        {
            _characterFactory.Create(_statsInfo, modelRoot => Instantiate(_modelPrefab, modelRoot));
            
            Destroy(gameObject);
        }
    }
}