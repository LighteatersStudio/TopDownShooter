using Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Views.Popups.CharacterSelectionMenu
{
    public class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _maxHealthText,
            _healthText,
            _moveSpeedText,
            _attackSpeed,
            _gunNameText,
            _shortPerSecondText,
            _ammoClipSizeTxt,
            _reloadTimeText,
            _damageText;

        [SerializeField] private GameObject _chooseCharacterView;
        [SerializeField] private GameObject _characteristicsView;

        private SelectCharacterService _selectCharacterService;


        [Inject]
        public void Construct(SelectCharacterService selectCharacterService)
        {
            _selectCharacterService = selectCharacterService;
        }

        public void Setup(bool isStats)
        {
            if (isStats)
            {
                _chooseCharacterView.gameObject.SetActive(false);
                _characteristicsView.gameObject.SetActive(true);
                
                SetupCharacteristics();
            }
            else
            {
                _chooseCharacterView.gameObject.SetActive(true);
                _characteristicsView.gameObject.SetActive(false);
            }
        }
        
        private void SetupCharacteristics()
        {
            var characterStats = _selectCharacterService.GetCharacterStats();
            var startWeaponStats = _selectCharacterService.GetStartWeaponStats();
            
            _maxHealthText.text = $"{characterStats.MaxHealth}";
            _healthText.text = $"{characterStats.Health}";
            _moveSpeedText.text = $"{characterStats.MoveSpeed}";
            _attackSpeed.text = $"{characterStats.AttackSpeed}";
            _gunNameText.text = $"{startWeaponStats.Id}";
            _shortPerSecondText.text = $"{startWeaponStats.ShotsPerSecond}";
            _ammoClipSizeTxt.text = $"{startWeaponStats.AmmoClipSize}";
            _reloadTimeText.text = $"{startWeaponStats.ReloadTime}";
            _damageText.text = $"{startWeaponStats.Damage}";
        }
    }
}