using UnityEngine;

namespace Gameplay.Collectables.FirstAid
{
    [CreateAssetMenu(fileName = "FirstAidKitSettings", menuName = "ArenaSettings/Consumable/FirstAidKit")]
    public class FirstAidKitSettings : ScriptableObject
    {
        [field: SerializeField] public float LifeTime { get; private set; } = 5f;
    }
}