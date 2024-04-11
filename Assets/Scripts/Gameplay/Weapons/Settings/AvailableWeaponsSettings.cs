using System.Linq;
using UnityEngine;

namespace Gameplay.Weapons
{
    [CreateAssetMenu(fileName = "AvailableWeaponsSettings", menuName = "ArenaSettings/Consumable/AvailableWeaponsSettings")]
    public class AvailableWeaponsSettings : ScriptableObject, IAvailableWeaponsSettings
    {
        [field: SerializeField] public float LifeTime { get; private set; } = 5f;
        [field: SerializeField] private WeaponSettings[] WeaponSettingsArray { get; set; }

        public WeaponSettings GetRandom()
        {
            if (!WeaponSettingsArray.Any())
            {
                Debug.LogError("No one weapon in AvailableWeaponsSettings");
            }
            var randomIndex = Random.Range(0, WeaponSettingsArray.Length);
            return WeaponSettingsArray[randomIndex];
        }
    }
}