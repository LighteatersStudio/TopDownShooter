using System.Collections.Generic;
using Loading;
using UI;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class AppLoader : MonoBehaviour
    {
        private IUIRoot _uiRoot;

        [Inject]
        public void Construct(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }

        protected void Start()
        {
            LoadGame();
        }

        private async void LoadGame()
        {
            var loadingScreen = _uiRoot.Open<LoadingScreen>();

            var loadingQueue = new Queue<ILoadingOperation>();
            loadingQueue.Enqueue(new MainMenuLoadingOperation());
            
            await loadingScreen.Load(loadingQueue);
        }
    }
}