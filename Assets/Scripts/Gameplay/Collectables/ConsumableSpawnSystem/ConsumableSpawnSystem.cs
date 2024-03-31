using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public class ConsumableSpawnSystem : IConsumableSpawnSystem, ISpawnSpaceRegister
    {
        private List<ISpawnSpace> _spawnSpaces = new();

        public Vector3 GetSpawnPoint()
        {
            if (_spawnSpaces.IsNullOrEmpty())
            {
                Debug.LogError("Spawn space not set");
                return Vector3.zero;
            }

            var randomSpawnSpace = _spawnSpaces[Random.Range(0, _spawnSpaces.Count - 1)];
            return randomSpawnSpace.GetRandomPoint();
        }

        public void Register(ISpawnSpace spawnSpace)
        {
            _spawnSpaces.Add(spawnSpace);
        }

        public void Unregister(ISpawnSpace spawnSpace)
        {
            _spawnSpaces.Remove(spawnSpace);
        }
    }
}