using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Infrastructure.Loading
{
    public class LoadArenaService : ILoadArenaService
    {
        private readonly ArenaListSettings _arenaListSettings;
        private TaskCompletionSource<bool> _currentTask;

        [Inject]
        public LoadArenaService(ArenaListSettings arenaListSettings)
        {
            _arenaListSettings = arenaListSettings;
        }

        public Task<bool> TryLoadArena(string name)
        {
            if (!Application.CanStreamedLevelBeLoaded(name))
            {
                Debug.LogAssertion($"Not valid scene name - {name}.");
                return Task.FromResult(false);
            }

            if (_currentTask != null)
            {
                Debug.LogAssertion("Scene load in process");
                return Task.FromResult(false);
            }

            var loadingOperation = SceneManager.LoadSceneAsync(name);
            _currentTask = new TaskCompletionSource<bool>();
            loadingOperation.completed += OnLoaded;

            return _currentTask.Task;

            void OnLoaded(AsyncOperation operation)
            {
                loadingOperation.completed -= OnLoaded;
                _currentTask.SetResult(true);
                _currentTask = null;

                Debug.Log($"LoadArenaService: {name} loaded");
            }
        }

        public async Task LoadRandomArena()
        {
            if (!_arenaListSettings.ArenaList.Any())
            {
                Debug.LogAssertion("There are no arenas on the list");
                return;
            }

            var randomAreaName = RandomAreaName(_arenaListSettings.ArenaList);
            var loadStarted = await TryLoadArena(randomAreaName);
            if (!loadStarted)
            {
                Debug.LogError($"Failed to load arend with name - {randomAreaName}.");
            }
        }

        private string RandomAreaName(IReadOnlyCollection<IArena> arenas)
        {
            var randomIndex = Random.Range(0, arenas.Count());
            return arenas.ElementAt(randomIndex).SceneName;
        }
    }
}