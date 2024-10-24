﻿using System;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class CameraTrackingTarget : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Quaternion _rotationOffset;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
            enabled = true;
        }

        public void ResetTarget()
        {
            _target = null;
        }

        private void FixedUpdate()
        {
            if (!_target)
            {
                enabled = false;
                return;
            }

            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.SetPositionAndRotation(smoothedPosition, _rotationOffset);
        }
    }
}