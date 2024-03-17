using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public interface ISpawnSpaceSetup
    {
        void SetArenaSpawnSpace(SpawnSpace spawnSpace);
        void RemoveSpawnSpace(SpawnSpace spawnSpace);
    }
}