using System.Collections;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationSpeed = 2f;
        
            
        private IPlayer _player;
        private Coroutine _updateProcess;

        private float _currentHealth;
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }

        private void Start()
        {
            _player.Health.HealthChanged += OnHealthChanged;
            _slider.value = _player.Health.HealthRelative;
        }

        private void OnDestroy()
        {
            _player.Health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _currentHealth = _player.Health.HealthRelative;


            if (_updateProcess != null)
            {
                StopCoroutine(_updateProcess);
            }
            _updateProcess = StartCoroutine(UpdateHealthBar(_currentHealth));
        }

        private IEnumerator UpdateHealthBar(float newValue)
        {
            var startValue = _slider.value;
            float speed = _animationSpeed / Mathf.Abs(newValue - startValue);
            float t = 0;

            while (t < 1)
            {
                _slider.value = Mathf.Lerp(startValue, newValue, t);
                t += speed * Time.deltaTime;
                yield return 0;
            }

            _slider.value = newValue;
        }
    }
}