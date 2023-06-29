using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Services.Loading.Implementation
{
    public class LoadingService : ILoadingService
    {
        private readonly ILoadingScreen _loadingScreen;

        [Inject]
        public LoadingService(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public async Task Load(Queue<ILoadingOperation> loadingOperations)
        {
            await _loadingScreen.Show(loadingOperations);
        }
    }
}