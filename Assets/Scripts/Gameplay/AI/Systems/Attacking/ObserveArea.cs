using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class ObserveArea : MonoBehaviour, IObserveArea
    {
        public event Action TargetsChanged;

        private readonly List<Transform> _targetsTransform = new();

        public IEnumerable<Transform> TargetsTransforms => _targetsTransform;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                _targetsTransform.Add(other.gameObject.transform);
                TargetsChanged?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() && _targetsTransform.Count != 0)
            {
                _targetsTransform.Clear();
                TargetsChanged?.Invoke();
            }
        }
    }
}