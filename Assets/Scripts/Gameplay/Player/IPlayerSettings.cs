using UnityEngine;

namespace Gameplay
{
    public interface IPlayerSettings
    {
        public StatsInfo Stats { get; }
        public GameObject Model { get; }
    }
}