using Gameplay.Weapons;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private WeaponUISetting _weaponUISetting;

        [Inject]
        public void Construct(WeaponUISetting weaponUI)
        {
            _weaponUISetting = weaponUI;
        }

        public void SetupWeaponOwner(IWeaponOwner weaponOwner)
        {
            _icon.sprite = _weaponUISetting.GetHudIcon(weaponOwner.Weapon);
        }
    }
}