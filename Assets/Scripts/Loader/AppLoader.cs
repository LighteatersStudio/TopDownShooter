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
        private MainMenuLoadingOperation _menuLoadingOperation;

        [Inject]
        public void Construct(IUIRoot uiRoot, MainMenuLoadingOperation menuLoadingOperation)
        {
            _uiRoot = uiRoot;
            _menuLoadingOperation = menuLoadingOperation;
        }

        protected void Start()
        {
            LoadGame();
        }

        private async void LoadGame()
        {
            var loadingScreen = _uiRoot.Open<LoadingScreen>();

            var loadingQueue = new Queue<ILoadingOperation>();
            loadingQueue.Enqueue(_menuLoadingOperation);
            
            await loadingScreen.Load(loadingQueue);
        }
    }
}