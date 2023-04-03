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
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IGameRun gameRun)
        {
            _uiRoot = uiRoot;
            _gameRun = gameRun;
            Debug.Log($"GameSessionStartScenario: gameRun = {_gameRun}");
        }
        
        
        protected void Start()
        {
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