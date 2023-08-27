﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.AI
{
    [RequireComponent(typeof(BoxCollider), typeof(SphereCollider))]
    public class ObserveArea : MonoBehaviour
    {
        private const float MovementThreshold = 0.01f;

        [SerializeField] [Range(0, 360)] private int _angle = 30;
        [SerializeField] private int _rotationSpeed = 2;

        private readonly List<Transform> _targetsTransforms = new();

        private Tween _rotationTween;
        private Vector3 _lastPosition;
        private BoxCollider _boxCollider;
        private SphereCollider _sphereCollider;

        public event Action TargetsChanged;
        public bool HasTarget => _targetsTransforms.Count > 0;
        public IEnumerable<Transform> TargetsTransforms => _targetsTransforms;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                foreach (var target in _targetsTransforms)
                {
                    if (other.gameObject.transform == target)
                    {
                        return;
                    }
                }

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

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _sphereCollider.enabled = false;
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

            if (_boxCollider.enabled)
            {
                ProcessRotation(isMoving);
            }
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

        public void ActivateAttackCollider()
        {
            _sphereCollider.enabled = true;
            _boxCollider.enabled = false;

            KillRotationTween();
        }

        public void DeactivateAttackCollider()
        {
            _sphereCollider.enabled = false;
            _boxCollider.enabled = true;
        }
    }
}