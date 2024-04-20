using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Character/Create CharacterMaterialColorChangeData", fileName = "CharacterMaterialColorChangeData", order = 0)]
    public class CharacterMaterialColorChangeData : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}