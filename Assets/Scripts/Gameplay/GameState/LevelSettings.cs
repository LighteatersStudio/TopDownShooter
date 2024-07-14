using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "LightEaters/GameSession/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public float LevelDurationS { get; private set; }
    }
}