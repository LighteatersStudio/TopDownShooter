using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EndPoint : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public void Construct(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _uiRoot.Open<WinLevelMenu>();
            }
        }
    }
}