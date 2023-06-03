using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Services.Loading
{
    public class LoadingService
    {
        private ILoadingScreen _loadingScreen;
        
        [Inject]
        public LoadingService(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        public async UniTask Load(ILoadingOperation loadingOperation)
        {
            var queue = new Queue<ILoadingOperation>();
            queue.Enqueue(loadingOperation);
            await _loadingScreen.Show(queue);
        }
        
        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            await _loadingScreen.Show(loadingOperations);
        }
    }
}