using System.Threading.Tasks;
using Infrastructure.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meta.Level.Shop
{
    public class LoadShopService : ILoadShopService
    {
        private readonly ShopSettings _shopSettings;
        private TaskCompletionSource<bool> _currentTask;

        public LoadShopService(ShopSettings shopSettings)
        {
            _shopSettings = shopSettings;
        }

        public Task Load()
        {
            var loadingOperation = SceneManager.LoadSceneAsync(_shopSettings.ShopSceneName);
            if (loadingOperation == null)
            {
                Debug.LogError("Loading operation was not initialised");
                return Task.FromResult(false);
            }

            _currentTask = new TaskCompletionSource<bool>();
            loadingOperation.completed += OnLoaded;

            return _currentTask.Task;

            void OnLoaded(AsyncOperation operation)
            {
                loadingOperation.completed -= OnLoaded;
                _currentTask.SetResult(true);
                _currentTask = null;

                Debug.Log($"LoadArenaService: {_shopSettings.ShopSceneName} loaded");
            }
        }
    }
}