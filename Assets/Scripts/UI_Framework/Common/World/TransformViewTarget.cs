using UnityEngine;

namespace UI.Framework
{
    public class TransformViewTarget : IViewTarget
    {
        private readonly Transform _transform;
        
        public Vector3 Position => _transform.position;

        public TransformViewTarget(Transform target)
        {
            _transform = target;
        }
        
        public TransformViewTarget(GameObject target)
        {
            _transform = target.transform;
        }
    }
}