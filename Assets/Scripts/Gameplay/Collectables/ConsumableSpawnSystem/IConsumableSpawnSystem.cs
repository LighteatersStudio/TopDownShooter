using UnityEngine;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public interface IConsumableSpawnSystem
    {
        Vector3 GetSpawnPoint();
    }
}