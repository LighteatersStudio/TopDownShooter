using UnityEngine;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    [CreateAssetMenu(fileName = "ConsumableSpawnSettings", menuName = "ArenaSettings/Consumable/ConsumableSpawnSettings")]
    public class ConsumableSpawnSettings : ScriptableObject
    {
        [field: SerializeField] public float DelaySpawn { get; private set; } = 15f;
    }
}