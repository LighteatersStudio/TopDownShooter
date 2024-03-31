using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    [RequireComponent(typeof(Collider))]
    public class SpawnSpace : MonoBehaviour, ISpawnSpace
    {
        [SerializeField] private Collider _spaceCollider;

        private ISpawnSpaceRegister _spawnSpaceRegister;

        [Inject]
        public void Construct(ISpawnSpaceRegister spawnSpaceRegister)
        {
            _spawnSpaceRegister = spawnSpaceRegister;
        }

        private void Start()
        {
            _spaceCollider = GetComponent<Collider>();
            _spawnSpaceRegister.Register(this);
        }

        public Vector3 GetRandomPoint()
        {
            Vector3 boundsMin = _spaceCollider.bounds.min;
            Vector3 boundsMax = _spaceCollider.bounds.max;

            float x = Random.Range(boundsMin.x, boundsMax.x);
            float z = Random.Range(boundsMin.z, boundsMax.z);
            float y = _spaceCollider.bounds.center.y;

            return new Vector3(x, y, z);
        }

        private void OnDestroy()
        {
            _spawnSpaceRegister.Unregister(this);
        }
    }
}