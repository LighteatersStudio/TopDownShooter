using System;
using System.Threading.Tasks;
using Services.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Loading
{
    public class LevelLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading Level...";

        private readonly SceneNames _sceneNames;
        private readonly ILoadArenaService _loadArenaService;

        [Inject]
        public LevelLoadingOperation(SceneNames sceneNames, ILoadArenaService loadArenaService)
        {
            _sceneNames = sceneNames;
            _loadArenaService = loadArenaService;
        }

        public async Task Launch(Action<float> progressHandler)
        {
            progressHandler?.Invoke(0.5f);

            bool loadStarted = await _loadArenaService.TryLoadArena(_sceneNames.LevelBaseSize);
            if (!loadStarted)
            {
                Debug.LogError($"Failed to start scene loading with name - {_sceneNames.LevelBaseSize}.");
            }

            progressHandler?.Invoke(1f);
        }

        public void AfterFinish()
        {
        }
    }
}