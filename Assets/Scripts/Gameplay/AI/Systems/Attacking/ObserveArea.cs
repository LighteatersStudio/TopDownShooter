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
        private bool _isMoving;
        
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
            transform.rotation = new Quaternion();
        }

        private void Update()
        {
            _isMoving = Mathf.Abs(transform.position.magnitude - _lastPosition.magnitude) > MovementThreshold ;
            _lastPosition = transform.position;

            ProcessRotation();
        }

        private void ProcessRotation()
        {
            if (_isMoving && _rotationTween.IsActive())
            {
                KillRotationTween();
                _isMoving = false;
            }
            else if (!_isMoving && !_rotationTween.IsActive())
            {
                RotationRight();
            }
        }
    }
}