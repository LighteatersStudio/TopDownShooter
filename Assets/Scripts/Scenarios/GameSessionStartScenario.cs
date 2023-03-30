using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class GameSessionStartScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public void Construct(IUIRoot uiRoot)
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