using UnityEngine;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public interface ISpawnSpace
    {
        Vector3 GetRandomPoint();
    }
}