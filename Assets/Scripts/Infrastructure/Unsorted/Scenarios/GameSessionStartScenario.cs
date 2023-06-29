using Services.Audio;
using UI;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace Infrastructure.Scenraios
{
    public class GameSessionStartScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        
        private IMusicPlayer _musicPlayer;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IMusicPlayer musicPlayer)
        {
            _uiRoot = uiRoot;
            _musicPlayer = musicPlayer;
        }
        
        protected void Start()
        {
            _musicPlayer.StopMusic();

            _uiRoot.Open<Hud>();
            _uiRoot.Open<StartLevelMenu>();

            Destroy(gameObject);
        }
    }
}