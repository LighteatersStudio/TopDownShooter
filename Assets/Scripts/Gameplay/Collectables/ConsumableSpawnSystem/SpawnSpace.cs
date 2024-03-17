using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.SpawnSystem
{
    [RequireComponent(typeof(Collider))]
    public class SpawnSpace : MonoBehaviour
    {
        public Collider SpaceCollider;

        private ISpawnSpaceSetup _spawnSpaceSetup;

        [Inject]
        public void Construct(ISpawnSpaceSetup spawnSpaceSetup)
        {
            _spawnSpaceSetup = spawnSpaceSetup;
        }

        private void Start()
        {
            SpaceCollider = GetComponent<Collider>();
            _spawnSpaceSetup.SetArenaSpawnSpace(this);
        }

        private void OnDestroy()
        {
            _spawnSpaceSetup.RemoveSpawnSpace(this);
        }
    }
}