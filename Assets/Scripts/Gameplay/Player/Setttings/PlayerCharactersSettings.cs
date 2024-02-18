using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Player/Create PlayerCharactersSettings", fileName = "PlayerCharactersSettings")]
    public class PlayerCharactersSettings: ScriptableObject, IPlayerCharactersSettings
    {
        [field: SerializeField] public PlayerSettings[] PlayerSettingsArray { get; private set; }
    }
}