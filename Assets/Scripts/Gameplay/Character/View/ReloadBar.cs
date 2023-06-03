using UnityEngine;
using UnityEngine.UI;
using Utility;
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
        [SerializeField] private float _reloadTime;
        
        private DynamicMonoInitializer<ICameraProvider> _initializer;
        private ICanReload _weaponOwner;
        
        
        [Inject]
        public void Construct(ICameraProvider cameraProvider, ICanReload weaponOwner)
        {
            _weaponOwner = weaponOwner;
            _initializer = new DynamicMonoInitializer<ICameraProvider>(cameraProvider);
        }
        
        protected void Start()
        {
            _initializer.Initialize(Initialize);
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
            
            OnReloaded();
        }

        private void OnReloaded()
        {
            _slider.value = _reloadTime;

            RefreshView();
        }

        private void RefreshView()
        {
            RefreshVisibility();
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