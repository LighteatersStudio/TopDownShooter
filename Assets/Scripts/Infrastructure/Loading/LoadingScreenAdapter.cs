using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Loading;
using UI;
using Zenject;

namespace Infrastructure.Loading
{
    public class LoadingScreenAdapter : ILoadingScreen
    {
        private readonly LoadingScreen.Factory _loadingScreenFactory;

        [Inject]
        public LoadingScreenAdapter(LoadingScreen.Factory loadingScreenFactory)
        {
            _loadingScreenFactory = loadingScreenFactory;
        }

        public async Task Show(Queue<ILoadingOperation> queue)
        {
            await _loadingScreenFactory.Open(queue).CloseAwaiter;
        }
    }
}