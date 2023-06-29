using System.Collections;
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
        [SerializeField] private float _reloadTime;
        
        private DynamicMonoInitializer<ICameraProvider> _initializer;
        private ICanReload _weaponOwner;
        private Coroutine _barFilling;
        
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

        private void OnReloaded()
        {
            if (_barFilling != null)
            {
                StopCoroutine(_barFilling);
            }
            
            _barFilling = StartCoroutine(BarFilling());
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

        private IEnumerator BarFilling()
        {
            _slider.gameObject.SetActive(true);
            
            _slider.value = 0;
            while (_slider.value < 1)
            {
                _slider.value += _reloadTime * Time.deltaTime;
                yield return 0;
            }
            
            RefreshVisibility();
        }

        public class Factory : PlaceholderFactory<ICanReload, ReloadBar>
        {
        }
    }
}