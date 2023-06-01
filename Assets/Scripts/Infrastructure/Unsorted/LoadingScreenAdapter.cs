using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Loading;
using UI;
using UI.Framework;
using Zenject;

namespace Infrastructure
{
    public class LoadingScreenAdapter : ILoadingScreen
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public LoadingScreenAdapter(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        public async Task Show(Queue<ILoadingOperation> queue)
        {
            await _uiRoot.Open<LoadingScreen>().Load(queue);
        }
    }
}