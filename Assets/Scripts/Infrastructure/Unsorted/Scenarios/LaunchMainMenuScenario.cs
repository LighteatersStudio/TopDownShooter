using Services.Audio;
using UI;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace Infrastructure.Scenraios
{
    public class LaunchMainMenuScenario : MonoBehaviour
    {
        private StartSplashScreen.Factory _splashScreenFactory;
        private TestSplashScreen.Factory _testSplashScreenFactory;
        private MainMenu.Factory _mainMenuFactory;
        private IMusicPlayer _musicPlayer;
        private IMusicList _musicList;

        [Inject]
        public void Construct(StartSplashScreen.Factory splashScreenFactory,
            TestSplashScreen.Factory testSplashScreenFactory,
            MainMenu.Factory mainMenuFactory,
            IMusicPlayer musicPlayer,
            IMusicList musicList)
        {
            _splashScreenFactory = splashScreenFactory;
            _testSplashScreenFactory = testSplashScreenFactory;
            _mainMenuFactory = mainMenuFactory;
            _musicPlayer = musicPlayer;
            _musicList = musicList;
        }

        protected void Start()
        {
            _musicPlayer.PlayMusic(_musicList.GetRandomTrack());

            var startSplashScreenView = _splashScreenFactory.Open();
            startSplashScreenView.Closed += OnSplashScreenClosed;
        }

        private void OnSplashScreenClosed(IView view)
        {
            view.Closed -= OnSplashScreenClosed;
            _mainMenuFactory.Open();
            _testSplashScreenFactory.Open();
            Destroy(gameObject);
        }
    }
}