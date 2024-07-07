using System;
using System.Threading.Tasks;
using Services.Loading;
using Zenject;

namespace Infrastructure.Loading
{
    public class GameRunShopLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading Shop...";

        private readonly ILoadShopService _loadShopService;

        [Inject]
        public GameRunShopLoadingOperation(ILoadShopService loadShopService)
        {
            _loadShopService = loadShopService;
        }

        public async Task Launch(Action<float> progressHandler)
        {
            progressHandler?.Invoke(0.5f);

            await _loadShopService.Load();

            progressHandler?.Invoke(1f);
        }

        public void AfterFinish()
        {
        }

        public class Factory : PlaceholderFactory<GameRunShopLoadingOperation>
        {
        }
    }
}