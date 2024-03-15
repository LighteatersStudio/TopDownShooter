using UnityEngine;

namespace Gameplay.Collectables.FirstAid
{
    [CreateAssetMenu(fileName = "FirstAidKitSettings", menuName = "ArenaSettings/Consumable/FirstAidKit")]
    public class FirstAidKitSettings : ScriptableObject, IFirstAidKitSettings, IFirstAidKitSpawnSettings
    {
        [field: SerializeField] public float LifeTime { get; private set; } = 5f;
        [field: SerializeField] public float DelaySpawn { get; private set; } = 15f;
    }
}