using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Services.Utility;
using Zenject;

namespace Gameplay.View
{
    public class ReloadBar: MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;

        [Header("Settings")]
        [SerializeField] private Vector3 _rootOffset;
        [SerializeField] private bool _hideOnBulletsAvailable = true;
        
        private DynamicMonoInitializer<ICameraProvider> _initializer;
        private IReloaded _source;
        private CooldownHandler _cooldown;
        
        [Inject]
        public void Construct(ICameraProvider cameraProvider, IReloaded source)
        {
            _source = source;
            _initializer = new DynamicMonoInitializer<ICameraProvider>(cameraProvider);
        }
        
        protected void Start()
        {
            _initializer.Initialize(Initialize);
            RefreshVisibility();
        }

        protected void OnDestroy()
        {
            _source.ReloadStarted -= OnReloaded;
        }

        private void Initialize(ICameraProvider cameraProvider)
        {
            transform.localPosition = _rootOffset;
            
            _source.ReloadStarted += OnReloaded;
            
            _canvas.worldCamera = cameraProvider.MainCamera;
            _slider.maxValue = 1;
        }

        private void OnReloaded(ICooldown cooldown)
        {
            _slider.gameObject.SetActive(true);
            
            _cooldown?.Break();
            _cooldown = new CooldownHandler(cooldown, OnProgressChanged, OnCompleted);

            void OnProgressChanged(float progress)
            {
                _slider.value = progress;
            }
            void OnCompleted()
            {
                RefreshVisibility();
            }
        }

        private void RefreshVisibility()
        {
            if (_hideOnBulletsAvailable)
            {
                _slider.gameObject.SetActive(false);
                return;
            }
            
            _slider.gameObject.SetActive(true);
        }

        public class Factory : PlaceholderFactory<IReloaded, ReloadBar>
        {
        }
    }
}