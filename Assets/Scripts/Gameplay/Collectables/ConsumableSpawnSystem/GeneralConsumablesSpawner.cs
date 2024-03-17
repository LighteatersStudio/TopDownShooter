using Gameplay.Collectables.FirstAid;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.SpawnSystem
{
    public class GeneralConsumablesSpawner : ITickable
    {
        private readonly ISpawner _firstAidKitSpawner;
        private readonly ConsumableSpawnSettings _consumableSpawnSettings;

        private float _timer = 0;

        [Inject]
        public GeneralConsumablesSpawner(FirstAidKitSpawner firstAidKitSpawner, ConsumableSpawnSettings consumableSpawnSettings)
        {
            _firstAidKitSpawner = firstAidKitSpawner;
            _consumableSpawnSettings = consumableSpawnSettings;
        }

        public void Tick()
        {
            if (_timer > _consumableSpawnSettings.DelaySpawn)
            {
                Spawn();
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
        }

        private void Spawn()
        {
            _firstAidKitSpawner.Spawn();
        }
    }
}