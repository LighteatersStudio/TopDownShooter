using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EndPoint : MonoBehaviour
    {
        private IGameState _gameState;
        
        [Inject]
        public void Construct(IGameState gameState)
        {
            _gameState = gameState;
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _gameState.Win();
            }
        }
    }
}