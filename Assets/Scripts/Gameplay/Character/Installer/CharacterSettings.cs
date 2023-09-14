using System;
using UnityEngine;

namespace Gameplay
{
    
    public class CharacterSettings : ICharacterSettings
    {
        public StatsInfo Stats { get; }
        public Func<Transform, GameObject> ModelFactory { get; }
        
        public CharacterSettings(StatsInfo stats, Func<Transform, GameObject> modelFactory)
        {
            Stats = stats;
            ModelFactory = modelFactory;
        }
    }
}