using System;
using Gameplay.Collectables.FirstAid;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public class ConsumablesSpawnInvoker : ITickable
    {
        private readonly ISpawner[] _spawners;
        private readonly IFirstAidKitSpawner _firstAidKitSpawner;
        private readonly ConsumableSpawnSettings _consumableSpawnSettings;

        private float _timer = 0;

        [Inject]
        public ConsumablesSpawnInvoker(IFirstAidKitSpawner firstAidKitSpawner, ConsumableSpawnSettings consumableSpawnSettings)
        {
            _consumableSpawnSettings = consumableSpawnSettings;
            _spawners = new ISpawner[] { firstAidKitSpawner };
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
            var count = _spawners.Length;
            if (count == 0)
            {
                return;
            }

            int index = Random.Range(0, _spawners.Length);
            _spawners[index].Spawn();
        }
    }
}