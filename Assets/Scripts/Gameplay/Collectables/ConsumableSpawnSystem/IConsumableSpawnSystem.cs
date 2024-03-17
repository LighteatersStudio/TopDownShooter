using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public interface IConsumableSpawnSystem
    {
        Vector3 GetSpawnPoint();
    }
}