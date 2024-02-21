using System;
using System.Linq;
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
                var playerSettingsList = _playerCharactersSettings.PlayerSettingsArray.ToList();

                return playerSettingsList[_index];
            }
        }

        [Inject]
        public void Construct(IPlayerCharactersSettings playerCharactersSettings)
        {
            _playerCharactersSettings = playerCharactersSettings;
        }
        
        public void SetPlayerSettings(int index)
        {
            if (index < 0 || index >= _playerCharactersSettings.PlayerSettingsArray.Count())
            {
                Debug.LogError("Index is out of range");
            }
            else
            {
                _index = index;
            }

            IndexSaved?.Invoke(_index);
        }
    }
}