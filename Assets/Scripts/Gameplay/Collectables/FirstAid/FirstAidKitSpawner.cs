using Gameplay.Collectables.SpawnSystem;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    public class FirstAidKitSpawner : ITickable
    {
        private readonly IConsumableSpawnPoint _consumableSpawnPoint;
        private readonly FirstAidKit.Factory _firstAidKitFactory;
        private readonly IFirstAidKitSpawnSettings _spawnSettings;

        private float _timer = 0;

        [Inject]
        public FirstAidKitSpawner(IConsumableSpawnPoint consumableSpawnPoint,
            FirstAidKit.Factory firstAidKitFactory,
            IFirstAidKitSpawnSettings spawnSettings)
        {
            _consumableSpawnPoint = consumableSpawnPoint;
            _firstAidKitFactory = firstAidKitFactory;
            _spawnSettings = spawnSettings;
        }

        public void Tick()
        {
            if (_timer > _spawnSettings.DelaySpawn)
            {
                _firstAidKitFactory.Create(_consumableSpawnPoint.GetSpawnPoint());
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
        }
    }
}