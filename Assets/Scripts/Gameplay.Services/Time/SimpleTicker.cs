using System;
using UnityEngine;

namespace Gameplay.Services.GameTime
{
    public class SimpleTicker : MonoBehaviour, ITicker
    {
        public event Action<float> Tick;

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }
    }
}