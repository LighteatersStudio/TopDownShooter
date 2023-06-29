using Gameplay.Weapons;
using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        private const string EmptyAmmoLabel = "empty";
        
        [SerializeField] private Image _icon;
        [Header("Bullets")]
        [SerializeField] private TMP_Text _bulletCount;
        [SerializeField] private Color _haveAmmoColor = Color.white;
        [SerializeField] private Color _emptyAmmoColor = Color.red;

        private WeaponUISetting _uiSetting;
        private IWeaponOwner _owner;
        private IWeaponReadonly _currentWeapon;
        

        [Inject]
        public void Construct(WeaponUISetting weaponSettings)
        {
            _uiSetting = weaponSettings;
        }

        public void SetupOwner(IWeaponOwner owner)
        {
            _owner = owner;
            
            OnWeaponChanged();
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

        private void OnWeaponChanged()
        {
            SubscribeToAmmo();
            
            OnAmmoChanged();
            RefreshView();
        }

        private void SubscribeToAmmo()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Ammo.RemainAmmoChanged -= OnAmmoChanged;
            }

            _currentWeapon = _owner.Weapon;
            _currentWeapon.Ammo.RemainAmmoChanged += OnAmmoChanged;
        }

        private void RefreshView()
        {
            _icon.sprite = _uiSetting.GetHudIcon(_owner.Weapon);
        }
        private void OnAmmoChanged()
        {
            if (_currentWeapon.Ammo.RemainAmmo == 0)
            {
                _bulletCount.text = EmptyAmmoLabel;
                _bulletCount.color = _emptyAmmoColor;
                return;
            }
            
            _bulletCount.text = _currentWeapon.Ammo.RemainAmmo.ToString();
            _bulletCount.color = _haveAmmoColor;
        }
    }
}