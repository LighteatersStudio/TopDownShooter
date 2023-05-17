using UnityEngine;

namespace Gameplay.Projectile
{
    public class MovementArc : MonoBehaviour, IProjectileMovement
    {
        private const float FlightTime = 10f;
        private const float TimeStep = 0.1f;
        private const float Gravity = 9.81f;

        [SerializeField] private int _range;
        [SerializeField] private float _speed;
        
        private Rigidbody _rigidbody;

        private float _tanTheta;
        private float _cosTheta;
        private float _sinTheta;
        private float _currenArcSpeed;

        private Vector3 _velocity;
        private Vector3 _position;
        private Vector2 _direction;

        private bool _lowTrajectorySelected;
        private bool _drawTrajectory;


        public void Move(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            transform.forward = direction;
            
            TrajectoryCalculation(_range, _speed);
            
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = _velocity;
        }

        /// <summary>
        /// The calculation formula for two trajectories is used : Link https://habr.com/ru/articles/538952/
        /// </summary>
        /// <param name="range"></param>
        /// <param name="speed"></param>
        private void TrajectoryCalculation(int range, float speed)
        {
            float height = -transform.position.y;

            float launchSpeed = Mathf.Sqrt(Gravity * (height + Mathf.Sqrt(range * range + height * height)));

            _currenArcSpeed = launchSpeed + speed;

            float s2 = _currenArcSpeed * _currenArcSpeed;
            float r = s2 * s2 - Gravity * (Gravity * range * range + 2f * height * s2);

            if (_lowTrajectorySelected)
            {
                _tanTheta = ((s2 - Mathf.Sqrt(Mathf.Abs(r))) / (Gravity * range));
            }
            else
            {
                _tanTheta = ((s2 + Mathf.Sqrt(Mathf.Abs(r))) / (Gravity * range));
            }

            _cosTheta = Mathf.Cos(Mathf.Atan(_tanTheta));
            _sinTheta = _cosTheta * _tanTheta;


            _direction = new Vector2(transform.forward.x, transform.forward.z);
            _position = transform.position;

            _velocity = new Vector3(_direction.x * _currenArcSpeed * _cosTheta,
                _currenArcSpeed * _sinTheta,
                _direction.y * _currenArcSpeed * _cosTheta);
        }

        private void DrawTrajectory()
        {
            Vector3 prev = _position, next;
            float time = 0f;

            while (time <= FlightTime)
            {
                float dx = _currenArcSpeed * _cosTheta * time;
                float dy = _currenArcSpeed * _sinTheta * time - 0.5f * Gravity * time * time;

                next = _position + new Vector3(_direction.x * dx, dy, _direction.y * dx);
                Debug.DrawLine(prev, next, Color.green);
                prev = next;
                time += TimeStep;

                if (next.y <= 0f)
                {
                    break;
                }
            }
        }

        private void Update()
        {
            if (_drawTrajectory)
            {
                DrawTrajectory();
            }
        }
    }
}