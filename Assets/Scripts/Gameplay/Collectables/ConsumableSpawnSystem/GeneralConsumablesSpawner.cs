using System.Collections.Generic;
using Gameplay.Collectables.FirstAid;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.SpawnSystem
{
    public class GeneralConsumablesSpawner : ITickable, IInitializable
    {
        private readonly List<ISpawner> _spawners = new();
        private readonly IFirstAidKitSpawner _firstAidKitSpawner;
        private readonly ConsumableSpawnSettings _consumableSpawnSettings;

        private float _timer = 0;

        [Inject]
        public GeneralConsumablesSpawner(IFirstAidKitSpawner firstAidKitSpawner, ConsumableSpawnSettings consumableSpawnSettings)
        {
            _firstAidKitSpawner = firstAidKitSpawner;
            _consumableSpawnSettings = consumableSpawnSettings;
        }

        public void Initialize()
        {
            _spawners.Add(_firstAidKitSpawner);
        }

        public void Tick()
        {
            if (_timer > _consumableSpawnSettings.DelaySpawn)
            {
                SpawnByRandomSpawner();
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
        }

        private void SpawnByRandomSpawner()
        {
            var count = _spawners.Count;
            if (count == 0)
            {
                return;
            }

            int index = Random.Range(0, _spawners.Count);
            _spawners[index].Spawn();
        }
    }
}