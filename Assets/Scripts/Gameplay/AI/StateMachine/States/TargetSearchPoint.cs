using System;
using UnityEngine;

namespace Gameplay.AI
{
    [Serializable]
    public class TargetSearchPoint
    {
        [SerializeField] private Vector3 _point;

        public Vector3 Point => _point;

        private TargetSearchPoint(Vector3 point)
        {
            _point = point;
        }
    }
}