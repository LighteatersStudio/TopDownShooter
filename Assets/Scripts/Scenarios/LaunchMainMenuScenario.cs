using Audio;
using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class LaunchMainMenuScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;

        private IMusicPlayer _musicPlayer;
        private IMusicList _musicList;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IMusicPlayer musicPlayer, IMusicList musicList)
        {
            _uiRoot = uiRoot;
            _musicPlayer = musicPlayer;
            _musicList = musicList;
        }

        protected void Start()
        {
            _musicPlayer.PlayMusic(_musicList.GetRandomTrack());
            
            var startSplashScreenView = _uiRoot.Open<StartSplashScreen>();
            startSplashScreenView.Closed += OnSplashScreenClosed;
        }
        
        private void OnSplashScreenClosed(IView view)
        {
            view.Closed -= OnSplashScreenClosed;
            _uiRoot.Open<MainMenu>();
            Destroy(gameObject);
        }
    }
}