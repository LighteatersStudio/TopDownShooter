using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Player/Create AllPlayersData", fileName = "AllPlayersData")]
    public class AllPlayersData: ScriptableObject
    {
        [field: SerializeField] public PlayerSettings[] PlayerSettingsArray { get; protected set; }
    }
}