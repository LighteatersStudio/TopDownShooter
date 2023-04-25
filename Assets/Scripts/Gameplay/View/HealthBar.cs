using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Gameplay.View
{
    public class HealthBar: MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderFiller;
        
        
        [Header("Settings")]
        [SerializeField] private Color _maxHealthColor = Color.green;
        [SerializeField] private Color _minHealthColor = Color.red;
        [SerializeField] private bool _hideOnFullHealth = true;
        
        private DynamicMonoInitializer<ICameraProvider, Transform> _initializer;
        private IHaveHealth _healthOwner;
        
        
        [Inject]
        public void Construct(ICameraProvider cameraProvider, IHaveHealth healthOwner, Transform parent)
        {
            _healthOwner = healthOwner;
            _initializer = new DynamicMonoInitializer<ICameraProvider, Transform>(cameraProvider, parent);
        }
        
        protected void Start()
        {
            _initializer.Initialize(Initialize);
        }
        
        private void Initialize(ICameraProvider cameraProvider, Transform parent)
        {
            transform.SetParent(parent, false);
            
            _healthOwner.HealthChanged += OnHealthChanged;
            
            _canvas.worldCamera = cameraProvider.MainCamera;
            _slider.maxValue = 1;
            
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            _slider.value = _healthOwner.HealthRelative;

            RefreshView();
        }

        private void RefreshView()
        {
            ChangeColor();
            RefreshVisibility();
        }
        
        private void RefreshVisibility()
        {
            if (!_hideOnFullHealth)
            {
                _slider.gameObject.SetActive(true);
                return;
            }
            
            _slider.gameObject.SetActive(Mathf.Abs(_slider.value - 1) > 0.001f);
        }

        private void ChangeColor()
        {
            _sliderFiller.color = Color.Lerp(_minHealthColor, _maxHealthColor, _slider.value);
        }

        public class Factory : PlaceholderFactory<IHaveHealth, Transform, HealthBar>
        {
        }
    }
}