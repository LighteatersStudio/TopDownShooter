using System;
using Gameplay.CollectableItems;
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
        private readonly IGameState _gameState;
        private Cooldown _spawnInvokeCooldown;
        private bool _isSpawn = false;

        public ConsumableSpawnersInvoker(IFirstAidKitSpawner firstAidKitSpawner,
            IWeaponSpawner weaponSpawner,
            ConsumableSpawnSettings consumableSpawnSettings,
            Cooldown.Factory cooldownFactory,
            IGameState gameState)
        {
            _consumableSpawnSettings = consumableSpawnSettings;
            _cooldownFactory = cooldownFactory;
            _gameState = gameState;
            _spawners = new ISpawner[] { firstAidKitSpawner, weaponSpawner };

            _spawnInvokeCooldown = _cooldownFactory.CreateFinished();

            _gameState.Won += OnLevelFinished;
            _gameState.PlayerDead += OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            _isSpawn = false;
            Dispose();
        }

        public void Initialize()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            _isSpawn = true;
            _spawnInvokeCooldown =
                _cooldownFactory.CreateWithCommonTicker(_consumableSpawnSettings.DelaySpawn, SpawnByRandomSpawner);
            _spawnInvokeCooldown.Launch();
        }

        private void SpawnByRandomSpawner()
        {
            if (!_isSpawn)
            {
                return;
            }

            var count = _spawners.Length;
            if (count == 0)
            {
                return;
            }

            int index = Random.Range(0, _spawners.Length);
            _spawners[index].Spawn();
            StartSpawn();
        }

        public void Dispose()
        {
            _spawnInvokeCooldown.ForceFinish();
            _spawnInvokeCooldown = null;
            _gameState.Won -= OnLevelFinished;
            _gameState.PlayerDead -= OnLevelFinished;
        }
    }
}