using System;
using System.Threading;
using UnityEngine;

namespace Gameplay.AI
{
    public class ObserveArea : MonoBehaviour, IObserveArea
    {
        public event Action<Transform> EnemyFound;
        public event Action EnemyEscaped;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                EnemyFound?.Invoke(other.gameObject.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                EnemyEscaped?.Invoke();
            }
        }
    }
}