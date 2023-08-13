using System;
using UnityEngine;

namespace Gameplay
{
    
    public class CharacterSettings : ICharacterSettings
    {
        public StatsInfo Stats { get; }
        public Func<Transform, GameObject> ModelFactory { get; }
        public TypeGameplayObject IsEnemy { get; }
        
        public CharacterSettings(StatsInfo stats, Func<Transform, GameObject> modelFactory, TypeGameplayObject isEnemy)
        {
            Stats = stats;
            ModelFactory = modelFactory;
            IsEnemy = isEnemy;
        }
    }
}