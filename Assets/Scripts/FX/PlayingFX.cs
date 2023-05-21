using UnityEngine;
using Zenject;

namespace FX
{
    public class PlayingFX : MonoBehaviour
    {
        private ParticleSystem _particlePrefab;
        private Vector3 _position;

        [Inject]
        public void Construct(ParticleSystem particlePrefab, Vector3 globalPosition)
        {
            _particlePrefab = particlePrefab;
            _position = globalPosition;
        }
        
        protected void Start()
        {
            transform.position = _position;
            
            var particle = Instantiate(_particlePrefab, transform);
            particle.Play();
            
            Destroy(gameObject, particle.main.startLifetime.constant);
        }
        
        public class Factory : PlaceholderFactory<ParticleSystem, Vector3, PlayingFX>
        {
        }
    }
}