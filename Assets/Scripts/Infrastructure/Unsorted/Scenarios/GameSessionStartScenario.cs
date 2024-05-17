using Services.Audio;
using UI;
using UI.Framework;
using UI.Views.Common;
using UnityEngine;
using Zenject;

namespace Infrastructure.Scenraios
{
    public class GameSessionStartScenario : MonoBehaviour
    {
        private TouchControlsView.Factory _touchControlFactory;
        private Hud.Factory _hudFactory;
        private StartLevelMenu.Factory _startLevelMenuFactory;

        private IMusicPlayer _musicPlayer;

        [Inject]
        public void Construct(TouchControlsView.Factory touchControlFactory,
            Hud.Factory hudFactory,
            StartLevelMenu.Factory startLevelMenuFactory,
            IMusicPlayer musicPlayer)
        {
            _touchControlFactory = touchControlFactory;
            _hudFactory = hudFactory;
            _startLevelMenuFactory = startLevelMenuFactory;
            _musicPlayer = musicPlayer;
        }

        protected void Start()
        {
            _musicPlayer.StopMusic();

            _touchControlFactory.Open();
            _hudFactory.Open();
            _startLevelMenuFactory.Open();

            Destroy(gameObject);
        }
    }
}