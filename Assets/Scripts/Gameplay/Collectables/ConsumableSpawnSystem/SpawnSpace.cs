using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public class SpawnSpace : MonoBehaviour, ISpawnSpace
    {
        [SerializeField] private List<Collider> _spaces;

        private ISpawnSpaceRegister _spawnSpaceRegister;

        [Inject]
        public void Construct(ISpawnSpaceRegister spawnSpaceRegister)
        {
            _spawnSpaceRegister = spawnSpaceRegister;
        }

        private void Start()
        {
            _spawnSpaceRegister.Register(this);
        }

        public Vector3 GetRandomPoint()
        {
            if (_spaces.IsNullOrEmpty())
            {
                Debug.LogAssertion("Collider list is empty");
                return Vector3.zero;
            }

            var randomCollider = _spaces[Random.Range(0, _spaces.Count)];
            return GetRandomPointInternal(randomCollider);
        }

        private Vector3 GetRandomPointInternal(Collider space)
        {
            Vector3 boundsMin = space.bounds.min;
            Vector3 boundsMax = space.bounds.max;

            float x = Random.Range(boundsMin.x, boundsMax.x);
            float z = Random.Range(boundsMin.z, boundsMax.z);
            float y = space.bounds.center.y;

            return new Vector3(x, y, z);
        }

        private void OnDestroy()
        {
            _spawnSpaceRegister.Unregister(this);
        }
    }
}