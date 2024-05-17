using UnityEngine;

namespace UI.Framework.Implementation
{
    [RequireComponent(typeof(Camera))]
    internal class WorldUICamera : MonoBehaviour, IWorldUICamera
    {
        public Transform Transform => transform;
        public Camera Source { get; private set; }

        private void Awake()
        {
            Source = GetComponent<Camera>();
        }
    }
}