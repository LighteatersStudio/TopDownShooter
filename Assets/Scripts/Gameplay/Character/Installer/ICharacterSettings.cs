using System;
using UnityEngine;

namespace Gameplay
{
    public interface ICharacterSettings
    {
        StatsInfo Stats { get; }
        Func<Transform, GameObject> ModelFactory { get; }
    }
}