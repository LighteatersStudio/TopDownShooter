using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Create CharacterFXList", fileName = "CharacterFXList", order = 0)]
    public class CharacterFXList : ScriptableObject, ICharacterFXList
    {
        [SerializeField] private ParticleSystem _hitFx;
        [SerializeField] private ParticleSystem _deadFx;

        public ParticleSystem HitFx => _hitFx;
        public ParticleSystem DeadFx => _deadFx;
    }
}