using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class ObserveArea : MonoBehaviour
    {
        public event Action TargetsChanged;

        private readonly List<Transform> _targetsTransforms = new();

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
                _targetsTransforms.Clear();
                TargetsChanged?.Invoke();
            }
        }
    }
}