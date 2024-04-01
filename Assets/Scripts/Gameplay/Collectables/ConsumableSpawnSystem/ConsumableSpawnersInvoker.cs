using System;
using Gameplay.Collectables.FirstAid;
using Gameplay.Services.GameTime;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public class ConsumableSpawnersInvoker : IInitializable, IDisposable
    {
        private readonly ISpawner[] _spawners;
        private readonly ConsumableSpawnSettings _consumableSpawnSettings;
        private readonly Cooldown.Factory _cooldownFactory;
        private Cooldown _spawnInvokeCooldown;

        [Inject]
        public ConsumableSpawnersInvoker(IFirstAidKitSpawner firstAidKitSpawner,
            ConsumableSpawnSettings consumableSpawnSettings,
            Cooldown.Factory cooldownFactory)
        {
            _consumableSpawnSettings = consumableSpawnSettings;
            _cooldownFactory = cooldownFactory;
            _spawners = new ISpawner[] { firstAidKitSpawner };

            _spawnInvokeCooldown = _cooldownFactory.CreateFinished();
        }

        public void Initialize()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            _spawnInvokeCooldown =
                _cooldownFactory.CreateWithCommonTicker(_consumableSpawnSettings.DelaySpawn, SpawnByRandomSpawner);
            _spawnInvokeCooldown.Launch();
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

        public void Dispose()
        {
            _spawnInvokeCooldown.ForceFinish();
            _spawnInvokeCooldown = null;
        }
    }
}