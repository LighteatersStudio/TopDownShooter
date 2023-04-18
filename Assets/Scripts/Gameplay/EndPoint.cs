using Level;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EndPoint : MonoBehaviour
    {
        private IGameRun _gameRun;
        
        [Inject]
        public void Construct(IGameRun gameRun)
        {
            _gameRun = gameRun;
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _gameRun.Finish();
            }
        }
    }
}