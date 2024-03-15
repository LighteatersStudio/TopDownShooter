using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.SpawnSystem
{
    [RequireComponent(typeof(Collider))]
    public class SpawnSpace : MonoBehaviour
    {
        [SerializeField] private Collider _spawnSpaceCollider;

        private ISpawnSpaceSetup _spawnSpaceSetup;

        [Inject]
        public void Construct(ISpawnSpaceSetup spawnSpaceSetup)
        {
            _spawnSpaceSetup = spawnSpaceSetup;
        }

        private void Start()
        {
            _spawnSpaceSetup.SetArenaSpawnSpace(_spawnSpaceCollider);
        }
    }
}