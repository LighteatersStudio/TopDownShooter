using Gameplay.Enemy;
using UnityEngine;


namespace Gameplay.Scenario.FirstCutsceneScenario
{
    [CreateAssetMenu(fileName = "ScenarioContainer", menuName = "LightEaters/Scenarios/ScenarioContainer")]
    public class ScenarioContainer : ScriptableObject
    {
        [field: SerializeField] private SimpleEnemySettings _enemySettings { get; set; }
        [field: SerializeField] private Transform _spawnEnemyPoint { get; set; }

        public SimpleEnemySettings EnemySettings => _enemySettings;
        public Transform SpawnEnemyPoint => _spawnEnemyPoint;
    }
}
