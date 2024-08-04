using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.AI
{
    [Serializable]
    public class MovingPath
    {
        [SerializeField] private List<Vector3> _points;
        [SerializeField] private bool _pingPong = true;

        public IEnumerable<Vector3> Points => _points;
        
        private MovingPath()
        {
        }

        private MovingPath(List<Vector3> points, bool pingPong)
        {
            _points = points;
            _pingPong = pingPong;
        }

        public MovingPath Reverse()
        {
            if (_pingPong)
            {
                return new MovingPath(_points.Reverse<Vector3>().ToList(), _pingPong);
            }

            return this;
        }
    }
}