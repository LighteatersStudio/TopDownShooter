using Cinemachine;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CameraProvider : ICameraProvider, ICinemachineBrainProvider
    {
        public Camera MainCamera { get; }
        public CinemachineBrain CinemachineBrain => _cinemachineBrain;

        private CinemachineBrain _cinemachineBrain;

        [Inject]
        public CameraProvider(Camera mainCamera)
        {
            MainCamera = mainCamera;
            SetCinemachineBrain();
        }

        private void SetCinemachineBrain()
        {
            if (!MainCamera.gameObject.TryGetComponent(out _cinemachineBrain))
            {
                _cinemachineBrain = MainCamera.gameObject.AddComponent<CinemachineBrain>();;
            }
        }
    }
}