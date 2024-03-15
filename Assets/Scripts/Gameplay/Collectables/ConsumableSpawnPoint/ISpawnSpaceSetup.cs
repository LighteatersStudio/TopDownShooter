using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public interface ISpawnSpaceSetup
    {
        void SetArenaSpawnSpace(Collider collider);
    }
}