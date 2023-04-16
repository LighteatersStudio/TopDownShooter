using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class StartPoint : MonoBehaviour
    {
        private IPlayer _player;
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            _player.SetPosition(transform.position); 
        }
    }
}