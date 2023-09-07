using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Create OutlineSettings", fileName = "OutlineSettings", order = 0)]
    public class OutlineSettings : ScriptableObject, IOutlineSettings
    {
        [field: SerializeField] public Color Player { get; private set; } = new(0.42f, 1, 0); 
        [field: SerializeField] public Color Enemy { get; private set; } = new(1f, 0.1f, 0);
        [field: SerializeField] public Color Neutral { get; private set; } = Color.cyan;
    }
}