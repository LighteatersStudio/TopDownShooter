using Gameplay.Services.GameTime;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private IGameTime _gameTime;
        
        [Inject]
        public void Construct(IGameTime gameTime)
        {
            _gameTime = gameTime;
        }

        private void Update()
        {
            _label.text = _gameTime.ConvertToString();
        }
    }
}