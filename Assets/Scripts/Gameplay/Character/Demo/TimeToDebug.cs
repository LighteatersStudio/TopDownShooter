using Zenject;
using UnityEngine;
using System;

namespace Gameplay.Demo
{
    public class TimeToDebug : ITickable
    {
        private float _timeAccumulator = 0f;
        private float _secondsPassed = 0f;

        public void Tick()
        {
            _timeAccumulator += Time.deltaTime;

            if (_timeAccumulator >= 1.0f)
            {
                _secondsPassed += _timeAccumulator;

                Debug.Log($"Time passed: {Math.Round(_secondsPassed, 2)} seconds");
                _timeAccumulator -= 1.0f;
            }
        }
    }
}