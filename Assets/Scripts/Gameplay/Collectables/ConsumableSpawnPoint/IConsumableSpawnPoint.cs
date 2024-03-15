using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public interface IConsumableSpawnPoint
    {
        Vector3 GetSpawnPoint();
    }
}