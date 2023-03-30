using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using Zenject;

namespace Loading
{
    public class LoadingService
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public LoadingService(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        public async UniTask Load(ILoadingOperation loadingOperation)
        {
            var queue = new Queue<ILoadingOperation>();
            queue.Enqueue(loadingOperation);
            
            await _uiRoot.Open<LoadingScreen>().Load(queue);
        }
        
        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            await _uiRoot.Open<LoadingScreen>().Load(loadingOperations);
        }
    }
}