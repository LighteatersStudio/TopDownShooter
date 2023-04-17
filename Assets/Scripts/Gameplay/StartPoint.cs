using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class StartPoint : MonoBehaviour
    {
        private const float OffsetY = 0.9070761f;

        private IPlayer _player;
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            _player.SetPosition(new Vector3(transform.position.x, transform.position.y + OffsetY, transform.position.z)); 
        }
    }
}