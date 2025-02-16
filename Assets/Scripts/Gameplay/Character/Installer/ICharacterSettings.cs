using UnityEngine;

namespace Gameplay
{
    public interface ICharacterSettings
    {
        StatsInfo Stats { get; }
        GameObject ModelPrefab { get; }
    }
}