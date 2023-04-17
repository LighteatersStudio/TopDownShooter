using System;
using UnityEngine;

namespace Gameplay
{
    public class EndPoint : MonoBehaviour
    {
        public event Action LevelPassed;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                LevelPassed?.Invoke();
            }
        }
    }
}