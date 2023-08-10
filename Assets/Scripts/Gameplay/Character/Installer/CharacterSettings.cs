using System;
using UnityEngine;

namespace Gameplay
{
    public class CharacterSettings
    {
        public readonly StatsInfo Stats;
        public readonly Func<Transform, GameObject> ModelFactory;
        public readonly TypeGameplayObject IsEnemy;

        public CharacterSettings(StatsInfo stats, Func<Transform, GameObject> modelFactory, TypeGameplayObject isEnemy)
        {
            Stats = stats;
            ModelFactory = modelFactory;
            IsEnemy = isEnemy;
        }
    }
}