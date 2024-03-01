using System;
using System.Threading.Tasks;
using Services.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Loading
{
    public class MainMenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading Main Menu...";

        private readonly SceneNames _sceneNames;

        [Inject]
        public MainMenuLoadingOperation(SceneNames sceneNames)
        {
            _sceneNames = sceneNames;
        }

        public async Task Launch(Action<float> progressHandler)
        {
            progressHandler?.Invoke(0.5f);

            var loadingOperation = SceneManager.LoadSceneAsync(_sceneNames.MainMenu);

            var taskResult = new TaskCompletionSource<bool>();
            loadingOperation.completed += OnLoaded;

            await taskResult.Task;

            void OnLoaded(AsyncOperation operation)
            {
                loadingOperation.completed -= OnLoaded;
                progressHandler?.Invoke(1f);
                taskResult.SetResult(true);
            }
        }

        public void AfterFinish()
        {
        }

        public class Factory : PlaceholderFactory<MainMenuLoadingOperation>
        {
        }
    }
}