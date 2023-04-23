using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Gameplay.View
{
    public class HealthBar: MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Canvas _canvas;
        
        [Header("Settings")]
        [SerializeField] private Color _maxhealthColor = Color.green;
        [SerializeField] private Color _minhealthColor = Color.red;
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
            _slider.value = _healthOwner.HealthRelative;
            RefreshView();
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
            }
            
            _slider.gameObject.SetActive(Mathf.Abs(_slider.value - 1) < 0.001f);
        }

        private void ChangeColor()
        {
            _slider.colors = new ColorBlock()
            {
                normalColor = Color.Lerp(_minhealthColor, _maxhealthColor, _slider.value),
                colorMultiplier = _slider.colors.colorMultiplier,
                disabledColor = _slider.colors.disabledColor,
                fadeDuration = _slider.colors.fadeDuration,
                highlightedColor = _slider.colors.highlightedColor,
                pressedColor = _slider.colors.pressedColor
            };
        }

        public class Factory : PlaceholderFactory<IHaveHealth, Transform, HealthBar>
        {
        }
    }
}