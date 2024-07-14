using UnityEngine;

namespace Meta.Level
{
    [CreateAssetMenu(fileName = "GameRunSettings", menuName = "LightEaters/GameSession/GameRunSettings")]
    public class GameRunSettings : ScriptableObject
    {
        [field: SerializeField] public int MaxLevel { get; private set; } = 1;
    }
}