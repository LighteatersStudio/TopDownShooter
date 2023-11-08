using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Services.FX
{
    public class PlayingFX : MonoBehaviour
    {
        private ParticleSystem _particlePrefab;
        private Vector3 _position;
        private Vector3 _direction;

        [Inject]
        public void Construct(ParticleSystem particlePrefab, FXContext context)
        {
            _particlePrefab = particlePrefab;
            _position = context.Position;
            _direction = context.Direction;
        }
        
        protected void Start()
        {
            transform.position = _position;

            var particle = Instantiate(_particlePrefab, transform);
            particle.transform.forward = _direction;
            particle.Play();
            
            Destroy(gameObject, particle.main.startLifetime.constant);
        }

        public class Factory : PlaceholderFactory<ParticleSystem, FXContext, PlayingFX>
        {
        }
    }
}
