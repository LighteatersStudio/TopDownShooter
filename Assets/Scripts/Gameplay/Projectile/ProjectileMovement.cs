using UnityEngine;

namespace Gameplay.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        private const float FlightTime = 10f;
        private const float TimeStep = 0.1f;
        private const float Gravity = 9.81f;

        private Rigidbody _rigidbody;
        
        private float _tanTheta;
        private float _cosTheta;
        private float _sinTheta;
        private float _currenArcSpeed;
        private float _speed;
        
        private Vector3 _velocity;
        private Vector3 _position;
        private Vector2 _direction;

        private bool _lowTrajectorySelected;
        private bool _drawTrajectory;

        private TrajectoryType _trajectoryType;

        
        public void Launch(int range, float speed, TrajectoryType trajectoryType)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _trajectoryType = trajectoryType;

            if (trajectoryType == TrajectoryType.InArc)
            {
                TrajectoryCalculation(range, speed);

                _rigidbody.velocity = _velocity;
            }
            else
            {
                _rigidbody.useGravity = false;
                _speed = speed;
            }
        }

        private void TrajectoryCalculation(int range, float speed)
        {
            float height = -transform.position.y;

            float launchSpeed = Mathf.Sqrt(Gravity * (height + Mathf.Sqrt(range * range + height * height)));

            _currenArcSpeed =  launchSpeed + speed;
            
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
                float dx = _currenArcSpeed * _cosTheta  * time;
                float dy = _currenArcSpeed * _sinTheta  * time - 0.5f * Gravity * time * time;
                
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
            if(_drawTrajectory && _trajectoryType == TrajectoryType.InArc)
            {
                DrawTrajectory();
            }
            else
            {
                transform.position += transform.forward * Time.deltaTime * _speed;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<Player>())
            {
                Destroy(gameObject);
            }
        }
    }
}