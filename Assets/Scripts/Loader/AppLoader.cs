using System.Threading.Tasks;
using Loading;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void LoadGame()
        {
            var loadingScreen = _uiRoot.Open<LoadingScreen>();
            loadingScreen.Load(new MainMenuLoading());
        }
    }
}