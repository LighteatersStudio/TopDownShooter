using System;
using System.Threading.Tasks;
using Services.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Loading
{
    public class ArenaLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading Level...";

        private readonly ILoadArenaService _loadArenaService;

        [Inject]
        public ArenaLoadingOperation(ILoadArenaService loadArenaService)
        {
            _loadArenaService = loadArenaService;
        }

        public async Task Launch(Action<float> progressHandler)
        {
            progressHandler?.Invoke(0.5f);

            await _loadArenaService.LoadRandomArena();

            progressHandler?.Invoke(1f);
        }

        public void AfterFinish()
        {
        }

        public class Factory : PlaceholderFactory<ArenaLoadingOperation>
        {
        }
    }
}