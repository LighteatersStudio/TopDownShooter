using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.Loading
{
    public class LoadArenaService : ILoadArenaService
    {
        private readonly ArenaLIstSettings _arenaLIstSettings;
        private TaskCompletionSource<bool> _currentTask;

        [Inject]
        public LoadArenaService(ArenaLIstSettings arenaLIstSettings)
        {
            _arenaLIstSettings = arenaLIstSettings;
        }

        public async Task<bool> TryLoadArena(string name)
        {
            if (!Application.CanStreamedLevelBeLoaded(name))
            {
                Debug.LogAssertion($"Not valid scene name - {name}.");
                return false;
            }

            if (_currentTask != null)
            {
                Debug.LogAssertion("Scene load in process");
                return false;
            }

            var loadingOperation = SceneManager.LoadSceneAsync(name);

            _currentTask = new TaskCompletionSource<bool>();
            loadingOperation.completed += OnLoaded;

            await _currentTask.Task;

            void OnLoaded(AsyncOperation operation)
            {
                loadingOperation.completed -= OnLoaded;
                _currentTask.SetResult(true);
                _currentTask = null;

                Debug.Log($"LoadArenaService: {name} loaded");
            }

            return true;
        }

        public async Task LoadRandomArena()
        {
            if (!_arenaLIstSettings.ArenaList.Any())
            {
                Debug.LogAssertion("There are no arenas on the list");
                return;
            }

            var randomAreaName = RandomAreaName(_arenaLIstSettings.ArenaList);
            await TryLoadArena(randomAreaName);
        }

        private string RandomAreaName(IEnumerable<IArena> arenas)
        {
            string randomAreaName = String.Empty;
            var randomIndex = Random.Range(0, arenas.Count());
            var index = 0;
            foreach (var arena in arenas)
            {
                if (index == randomIndex)
                {
                    randomAreaName = arena.SceneName;
                    break;
                }

                index++;
            }

            return randomAreaName;
        }
    }
}