using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "LightEaters/Weapon/Create WeaponUISetting", fileName = "WeaponUISetting", order = 0)]
    public class WeaponUISetting : ScriptableObject
    {
        [SerializeField] private List<WeaponUI> _weaponIcons;

        public Sprite GetHudIcon(IWeaponReadonly weapon)
        {
            foreach (var weaponIcon in _weaponIcons)
            {
                if (weaponIcon.WeaponType == weapon.WeaponType)
                {
                    return weaponIcon.Icon;
                }
            }
            
            Debug.LogError($"not found icon: {weapon.WeaponType}");
            return null;
        }
    }
}