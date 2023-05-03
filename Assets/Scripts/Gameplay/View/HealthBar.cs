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
        [SerializeField] private Vector3 _rootOffest = new(0, 1.5f, 0);
        [SerializeField] private bool _hideOnFullHealth = true;
        [SerializeField] private bool _hideOnEmptyHealth = true;
        
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
            transform.localPosition = _rootOffest;
            
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
            if (Mathf.Abs(_slider.value - 1) < 1e-5 && _hideOnFullHealth)
            {
                _slider.gameObject.SetActive(false);
                return;
            }

            if (Mathf.Abs(_slider.value - 1) > 1 - 1e-5 && _hideOnEmptyHealth)
            {
                _slider.gameObject.SetActive(false);
                return;
            }

            _slider.gameObject.SetActive(true);
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