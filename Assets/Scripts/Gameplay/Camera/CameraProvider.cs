using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CameraProvider : ICameraProvider
    {
        public Camera MainCamera { get; }


        [Inject]
        public CameraProvider(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }
    }
}