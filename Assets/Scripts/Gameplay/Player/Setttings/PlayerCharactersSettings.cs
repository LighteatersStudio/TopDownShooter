using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Player/Create PlayerCharactersSettings", fileName = "PlayerCharactersSettings")]
    public class PlayerCharactersSettings: ScriptableObject, IPlayerCharactersSettings
    {
        [SerializeField] private PlayerSettings[] _playerSettingsArray;
        IEnumerable<PlayerSettings> IPlayerCharactersSettings.PlayerSettingsArray => _playerSettingsArray;
    }
}