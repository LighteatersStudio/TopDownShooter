using UnityEngine;

namespace Gameplay
{
    public interface ICharacterFXList
    {
        public ParticleSystem HitFx { get; }
        public ParticleSystem DeadFx { get; }
    }
}