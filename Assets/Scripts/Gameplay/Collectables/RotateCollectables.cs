using System;
using UnityEngine;
using DG.Tweening;

namespace Gameplay.Collectables
{
    public class RotateCollectables : MonoBehaviour
    {
        [SerializeField] private float _durationS = 2f;
        private readonly Vector3 _rotationAxis = Vector3.up;
        private Tweener _rotationTweener;

        private void Start()
        {
            RotateObject();
        }

        public void StopRotation()
        {
            _rotationTweener.Kill();
        }

        private void RotateObject()
        {
            _rotationTweener = transform.DORotate(_rotationAxis * 360f, _durationS, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        private void OnDestroy()
        {
            StopRotation();
        }
    }
}