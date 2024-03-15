using UnityEngine;

namespace Gameplay.Collectables.SpawnSystem
{
    public class ConsumableSpawnPoint : IConsumableSpawnPoint, ISpawnSpaceSetup
    {
        private Collider _spawnSpaceCollider;

        public Vector3 GetSpawnPoint()
        {
            if (_spawnSpaceCollider == null)
            {
                Debug.LogError("Spawn space not set");
                return Vector3.zero;
            }

            Vector3 boundsMin = _spawnSpaceCollider.bounds.min;
            Vector3 boundsMax = _spawnSpaceCollider.bounds.max;

            float x = Random.Range(boundsMin.x, boundsMax.x);
            float z = Random.Range(boundsMin.z, boundsMax.z);
            float y = _spawnSpaceCollider.bounds.center.y;

            return new Vector3(x, y, z);
        }

        public void SetArenaSpawnSpace(Collider collider)
        {
            _spawnSpaceCollider = collider;
            Debug.Log("Spawn space is set");
        }
    }
}