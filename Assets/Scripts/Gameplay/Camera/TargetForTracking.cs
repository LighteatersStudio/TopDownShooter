using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class TargetForTracking: MonoBehaviour
    {
        private CameraTrackingTarget _cameraTrackingTarget;
        
        
        [Inject]
        public void Construct(CameraTrackingTarget cameraTrackingTarget)
        {
            _cameraTrackingTarget = cameraTrackingTarget;
        }

        protected void Start()
        {
            _cameraTrackingTarget.SetTarget(transform);
        }
    }
}