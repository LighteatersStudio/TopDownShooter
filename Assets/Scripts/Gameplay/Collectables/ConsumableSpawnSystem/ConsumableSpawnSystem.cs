using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public class ConsumableSpawnSystem : IConsumableSpawnSystem, ISpawnSpaceSetup
    {
        private List<SpawnSpace> _spawnSpaces = new();

        public Vector3 GetSpawnPoint()
        {
            if (_spawnSpaces.IsNullOrEmpty())
            {
                Debug.LogError("Spawn space not set");
                return Vector3.zero;
            }

            var spaceCollider = _spawnSpaces[Random.Range(0, _spawnSpaces.Count - 1)].SpaceCollider;

            Vector3 boundsMin = spaceCollider.bounds.min;
            Vector3 boundsMax = spaceCollider.bounds.max;

            float x = Random.Range(boundsMin.x, boundsMax.x);
            float z = Random.Range(boundsMin.z, boundsMax.z);
            float y = spaceCollider.bounds.center.y;

            return new Vector3(x, y, z);
        }

        public void SetArenaSpawnSpace(SpawnSpace spawnSpace)
        {
            _spawnSpaces.Add(spawnSpace);
            Debug.Log("Spawn space is set");
        }

        public void RemoveSpawnSpace(SpawnSpace spawnSpace)
        {
            _spawnSpaces.Remove(spawnSpace);
        }
    }
}