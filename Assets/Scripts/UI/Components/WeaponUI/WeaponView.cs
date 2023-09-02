using Gameplay.Services.GameTime;
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
        [SerializeField] private Color _haveAmmoColor = Color.black;
        [SerializeField] private Color _emptyAmmoColor = Color.red;

        private WeaponUISetting _uiSetting;
        private IWeaponOwner _owner;
        private IWeaponReadonly _currentWeapon;
        private CooldownHandler _reloadCooldown;
        

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
                _currentWeapon.ShotDone -= OnAmmoChanged;
                _currentWeapon.ReloadStarted -= OnWeaponReloadStarted;
            }

            _currentWeapon = _owner.Weapon;
            _currentWeapon.ShotDone += OnAmmoChanged;
            _currentWeapon.ReloadStarted += OnWeaponReloadStarted;
        }

        private void RefreshView()
        {
            _icon.sprite = _uiSetting.GetHudIcon(_owner.Weapon);
        }
        private void OnAmmoChanged()
        {
            if (_currentWeapon.RemainAmmo == 0)
            {
                _bulletCount.text = EmptyAmmoLabel;
                _bulletCount.color = _emptyAmmoColor;
                return;
            }
            
            _bulletCount.text = _currentWeapon.RemainAmmo.ToString();
            _bulletCount.color = _haveAmmoColor;
        }

        private void OnWeaponReloadStarted(ICooldown cooldown)
        {
            _reloadCooldown?.Break();
            _reloadCooldown = new CooldownHandler(cooldown, OnProgressChanged, OnCompleted);

            void OnProgressChanged(float progress)
            {
                _icon.fillAmount = 1 - progress;
            }

            void OnCompleted()
            {
                _icon.fillAmount = 1;
                OnAmmoChanged();
            }
        }
    }
}