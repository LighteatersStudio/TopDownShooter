using Gameplay.Services.GameTime;
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
        private ICanReload _weaponOwner;
        private CooldownHandler _cooldown;
        
        [Inject]
        public void Construct(ICameraProvider cameraProvider, ICanReload weaponOwner)
        {
            _weaponOwner = weaponOwner;
            _initializer = new DynamicMonoInitializer<ICameraProvider>(cameraProvider);
        }
        
        protected void Start()
        {
            _initializer.Initialize(Initialize);
            RefreshVisibility();
        }

        protected void OnDestroy()
        {
            _weaponOwner.Reloaded -= OnReloaded;
        }

        private void Initialize(ICameraProvider cameraProvider)
        {
            transform.localPosition = _rootOffset;
            
            _weaponOwner.Reloaded += OnReloaded;
            
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

        public class Factory : PlaceholderFactory<ICanReload, ReloadBar>
        {
        }
    }
}