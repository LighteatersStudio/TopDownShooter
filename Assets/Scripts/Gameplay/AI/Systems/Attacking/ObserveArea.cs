using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.AI
{
    public class ObserveArea : MonoBehaviour
    {
        private const float MovementThreshold = 0.01f;
        
        [SerializeField][Range(0,360)] private int _angle = 30;
        [SerializeField] private int _rotationSpeed = 2;

        private readonly List<Transform> _targetsTransforms = new();
        
        private Tween _rotationTween;
        private Vector3 _lastPosition;
        
        public event Action TargetsChanged;
        public bool HasTarget => _targetsTransforms.Count > 0;
        public IEnumerable<Transform> TargetsTransforms => _targetsTransforms;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                _targetsTransforms.Add(other.gameObject.transform);
                TargetsChanged?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() && _targetsTransforms.Count != 0)
            {
                _targetsTransforms.Remove(other.gameObject.transform);
                TargetsChanged?.Invoke();
            }
        }

        private void Start()
        {
            _lastPosition = transform.position;
        }
        
        private void RotationRight()
        {
            _rotationTween = transform.DOLocalRotate(new Vector3(0f, _angle, 0f), _rotationSpeed, RotateMode.Fast)
                .OnComplete(RotationLeft);
        }
        
        private void RotationLeft()
        {
            _rotationTween = transform.DOLocalRotate(new Vector3(0f, -_angle, 0f), _rotationSpeed, RotateMode.Fast)
                .OnComplete(RotationRight);
        }
        
        private void KillRotationTween()
        {
            _rotationTween.Kill();
            transform.localRotation = Quaternion.identity;
        }

        private void Update()
        {
            var isMoving = Vector3.Distance(transform.position, _lastPosition) > MovementThreshold;
            _lastPosition = transform.position;

            ProcessRotation(isMoving);
        }

        private void ProcessRotation(bool isMoving)
        {
            if (isMoving && _rotationTween.IsActive())
            {
                KillRotationTween();
            }
            else if (!isMoving && !_rotationTween.IsActive())
            {
                RotationRight();
            }
        }
    }
}