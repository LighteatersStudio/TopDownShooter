﻿using UnityEngine;
using DG.Tweening;

namespace Gameplay.Collectables
{
    public class RotateCollectables : MonoBehaviour
    {
        private const float RotationSpeed = 2f;
        private readonly Vector3 _rotationAxis = Vector3.up;
        private Tweener _rotationTweener;

        private void Start()
        {
            RotateObject();
        }

        private void RotateObject()
        {
            _rotationTweener = transform.DORotate(_rotationAxis * 360f, RotationSpeed, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        public void StopRotation()
        {
            _rotationTweener.Kill();
        }
    }
}