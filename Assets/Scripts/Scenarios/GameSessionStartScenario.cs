using Audio;
using Cysharp.Threading.Tasks;
using Level;
using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class GameSessionStartScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        private IGameRun _gameRun;
        
        private IMusicPlayer _musicPlayer;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IGameRun gameRun, IMusicPlayer musicPlayer)
        {
            _uiRoot = uiRoot;
            _gameRun = gameRun;
            _musicPlayer = musicPlayer;
        }
        
        
        protected void Start()
        {
            _musicPlayer.StopMusic();
            
            _uiRoot.Open<Hud>();
            var view = _uiRoot.Open<HighStoneChooseMenu>();
            view.Closed += OnHighStoneChooseMenuClosed;
            Destroy(gameObject);
        }

        private async void OnHighStoneChooseMenuClosed(IView obj)
        {
            await UniTask.Delay(5000);
            await _gameRun.Finish();
        }
    }
}