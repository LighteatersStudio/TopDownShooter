using UnityEngine;
using System;
using Zenject;

namespace Gameplay.Demo
{
    public class TimeToDebugMono : MonoBehaviour
    {
        private IHaveHealth _health;

        private float _timeAccumulator = 0f;
        private float _secondsPassed = 0f;
        private DateTime _startTimePoint;

        [Inject]
        public void Construct(DateTime startTimePoint, IHaveHealth health)
        {
            _startTimePoint = startTimePoint;
            _health = health;
            Debug.Log($"Starting point: {_startTimePoint}");
        }

        private void Update()
        {
            _timeAccumulator += Time.deltaTime;

            if (_timeAccumulator >= 1.0f)
            {
                _secondsPassed += _timeAccumulator;

                Debug.Log($"Time passed: {Math.Round(_secondsPassed, 2)} seconds since {_startTimePoint}");
                Debug.Log($"Health {_health.HealthRelative}");
                _timeAccumulator -= 1.0f;
            }
        }
    }
}