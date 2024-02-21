using System.Text;
using Gameplay;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Views.Popups.CharacterSelectionMenu
{
    public class CharacterStatsView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _textStats;
        
        private SelectCharacterService _selectCharacterService;
        
        
        [Inject]
        public void Construct(SelectCharacterService selectCharacterService)
        {
            _selectCharacterService = selectCharacterService;
        }

        [Button]
        void SetupCharacteristics()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            var characterStats = _selectCharacterService.GetCharacterStats(); 
            var startWeaponStats = _selectCharacterService.GetStartWeaponStats();

            stringBuilder.AppendLine("Character Stats");
            stringBuilder.AppendLine("MaxHealth: " + $"{characterStats.MaxHealth}");
            stringBuilder.AppendLine("Health: " + $"{characterStats.Health}");
            stringBuilder.AppendLine("Move Speed: " + $"{characterStats.MoveSpeed}");
            stringBuilder.AppendLine("Attack Speed: " + $"{characterStats.AttackSpeed}");
            stringBuilder.AppendLine("Start Weapon Stats");
            stringBuilder.AppendLine("Gun: " + $"{startWeaponStats.Id}");
            stringBuilder.AppendLine("Shot Per Second: " + $"{startWeaponStats.ShotsPerSecond}");
            stringBuilder.AppendLine("Ammo Clip Size: " + $"{startWeaponStats.AmmoClipSize}");
            stringBuilder.AppendLine("Reload Time: " + $"{startWeaponStats.ReloadTime}");
            stringBuilder.AppendLine("Damage: " + $"{startWeaponStats.Damage}");
           
            
            string result = stringBuilder.ToString();
            
            _textStats.text = result;
        }
    }
}