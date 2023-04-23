using UnityEngine;

namespace Gameplay
{
    public class CameraProvider : ICameraProvider
    {
        public Camera MainCamera => Camera.main;
    }
}