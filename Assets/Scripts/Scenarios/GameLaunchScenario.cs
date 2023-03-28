using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class GameLaunchScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public void Construct(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        
        protected void Start()
        {
            _uiRoot.Open<Hud>();
            //var view = _uiRoot.Open<HighStoneChooseMenu>();
            
            Destroy(gameObject);
        }
    }
}