using Gameplay;
using TMPro;
using UnityEngine;

namespace UI.Views.Popups.CharacterSelectionMenu
{
    public class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _maxHealthText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _moveSpeedText;
        [SerializeField] private TMP_Text _attackSpeed;
        [SerializeField] private TMP_Text _gunNameText;
        [SerializeField] private TMP_Text _shortPerSecondText;
        [SerializeField] private TMP_Text _ammoClipSizeTxt;
        [SerializeField] private TMP_Text _reloadTimeText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private GameObject _chooseCharacterView;
        [SerializeField] private GameObject _characteristicsView;


        public void Setup(bool isStats, PlayerSettings playerSettings)
        {
            if (isStats)
            {
                _chooseCharacterView.gameObject.SetActive(false);
                _characteristicsView.gameObject.SetActive(true);

                SetupCharacteristics(playerSettings);
            }
            else
            {
                _chooseCharacterView.gameObject.SetActive(true);
                _characteristicsView.gameObject.SetActive(false);
            }
        }

        private void SetupCharacteristics(PlayerSettings playerSettings)
        {
            var characterStats = playerSettings.Stats;
            var startWeaponStats = playerSettings.DefaultWeapon;

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