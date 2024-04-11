using Gameplay.Collectables.ConsumableSpawnSystem;

namespace Gameplay.CollectableItems
{
    public class WeaponSpawner : IWeaponSpawner
    {
        private readonly IConsumableSpawnSystem _consumableSpawnSystem;
        private readonly WeaponCollectable.Factory _weaponCollectableFactory;

        public WeaponSpawner(IConsumableSpawnSystem consumableSpawnSystem,WeaponCollectable.Factory weaponCollectableFactory)
        {
            _consumableSpawnSystem = consumableSpawnSystem;
            _weaponCollectableFactory = weaponCollectableFactory;
        }

        public void Spawn()
        {
            _weaponCollectableFactory.Create(_consumableSpawnSystem.GetSpawnPoint());
        }
    }
}