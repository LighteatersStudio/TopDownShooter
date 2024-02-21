using System;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class SelectCharacterService
    {
        private IPlayerCharactersSettings _playerCharactersSettings;
        private int _index;
        public event Action<int> IndexSaved;
        public PlayerSettings GetPlayerSettings
        {
            get
            {
                if (_index < 0 || _index >= _playerCharactersSettings.PlayerSettingsArray.Length)
                {
                    Debug.LogError("Index is out of range");
                }

                return _playerCharactersSettings.PlayerSettingsArray[_index];
            }
        }

        [Inject]
        public void Construct(IPlayerCharactersSettings playerCharactersSettings)
        {
            _playerCharactersSettings = playerCharactersSettings;
        }
        
        public void SetPlayerSettings(int index)
        {
            _index = index;
            
            IndexSaved?.Invoke(_index);
        }

        public StatsInfo GetCharacterStats()
        {
            return _playerCharactersSettings.PlayerSettingsArray[_index].Stats;
        }
        
        public IWeaponSettings GetStartWeaponStats()
        {
            return  _playerCharactersSettings.PlayerSettingsArray[_index].DefaultWeapon;
        }
    }
}