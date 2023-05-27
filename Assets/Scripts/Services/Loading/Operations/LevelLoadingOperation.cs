using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services.Loading
{
    public class LevelLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading Level...";

        private readonly SceneNames _sceneNames;
        
        [Inject]
        public LevelLoadingOperation(SceneNames sceneNames)
        {
            _sceneNames = sceneNames;
        }
        
        public async UniTask Launch(Action<float> progressHandler)
        {
            progressHandler?.Invoke(0.5f);

            var loadingOperation = SceneManager.LoadSceneAsync(_sceneNames.Level1);
            
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
    }
}