using Gameplay.Collectables.SpawnSystem;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    public class FirstAidKitSpawner : IFirstAidKitSpawner
    {
        private readonly IConsumableSpawnSystem _consumableSpawnSystem;
        private readonly FirstAidKit.Factory _firstAidKitFactory;

        [Inject]
        public FirstAidKitSpawner(IConsumableSpawnSystem consumableSpawnSystem,
            FirstAidKit.Factory firstAidKitFactory)
        {
            _consumableSpawnSystem = consumableSpawnSystem;
            _firstAidKitFactory = firstAidKitFactory;
        }

        public void Spawn()
        {
            _firstAidKitFactory.Create(_consumableSpawnSystem.GetSpawnPoint());
        }
    }
}