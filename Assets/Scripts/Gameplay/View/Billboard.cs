using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class Billboard : MonoBehaviour
    {
        private ICameraProvider _cameraProvider;
        private Transform _mainCameraTransform;

        [Inject]
        public void Construct(ICameraProvider cameraProviderProvider)
        {
            _cameraProvider = cameraProviderProvider;
        }

        protected void LateUpdate()
        {
            if(_mainCameraTransform == null)
            {
                _mainCameraTransform = _cameraProvider.MainCamera.transform;
            }
            
            transform.LookAt(transform.position + _mainCameraTransform.rotation * Vector3.forward,
                _mainCameraTransform.rotation * Vector3.up);
        }
    }
}