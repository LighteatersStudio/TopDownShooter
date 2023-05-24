using Gameplay.Services.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.GameTime
{
    public class GameTimer : MonoBehaviour, IGameTime
    {
        private IPause _pause;

        private float _time;
        
        public float Value => _time;

        [Inject]
        public void Construct(IPause pause)
        {
            _pause = pause;
        }
        
        private void Update()
        {
            if (_pause.Paused)
            {
                return;
            }
            
            _time += Time.deltaTime;
        }
    }
}