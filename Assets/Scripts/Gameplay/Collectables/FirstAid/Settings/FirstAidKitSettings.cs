using UnityEngine;

namespace Gameplay.Collectables.FirstAid
{
    [CreateAssetMenu(fileName = "FirstAidKitSettings", menuName = "ArenaSettings/Consumable/FirstAidKitSettings")]
    public class FirstAidKitSettings : ScriptableObject
    {
        [field: SerializeField] public float LifeTime { get; private set; } = 5f;
        [field: SerializeField] public float HpUpAmount { get; private set; } = 15f;
    }
}