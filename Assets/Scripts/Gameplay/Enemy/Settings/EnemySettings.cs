using System;
using Gameplay.AI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Enemy
{
    [Serializable]
    public class EnemySettings<TEnemyAI> : ICharacterSettings where TEnemyAI : IAIBehaviourInstaller
    {
        [SerializeField] private StatsInfo _statsInfo = new() {MaxHealth = 100, Health = 100, MoveSpeed = 1, AttackSpeed = 1};
        [SerializeField] private GameObject _modelPrefab;
        [SerializeField] private TEnemyAI _simpleEnemyAI;
        
        public StatsInfo Stats => _statsInfo;
        public Func<Transform, GameObject> ModelFactory => parent => Object.Instantiate(_modelPrefab, parent);
        
        public TEnemyAI SimpleEnemyAI => _simpleEnemyAI;
    }
}