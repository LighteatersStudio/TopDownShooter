using Audio;
using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
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