using Gameplay.Weapons;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private WeaponUISetting _uiSetting;
        private IWeaponOwner _owner;

        
        [Inject]
        public void Construct(WeaponUISetting weaponSettings)
        {
            _uiSetting = weaponSettings;
        }

        public void SetupOwner(IWeaponOwner owner)
        {
            _owner = owner;
            
            RefreshView(_owner.Weapon);
            Unsubscribe();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _owner.WeaponChanged += OnWeaponChanged;
        }

        private void Unsubscribe()
        {
            if (_owner != null)
            {
                _owner.WeaponChanged -= OnWeaponChanged;    
            }
        }

        private void OnWeaponChanged(IWeaponReadonly oldWeapon, IWeaponReadonly newWeapon)
        {
            RefreshView(newWeapon);
        }
        
        private void RefreshView(IWeaponReadonly weapon)
        {
            _icon.sprite = _uiSetting.GetHudIcon(weapon);
        }
    }
}