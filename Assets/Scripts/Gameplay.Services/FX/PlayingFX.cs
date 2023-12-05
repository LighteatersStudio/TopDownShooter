using UnityEngine;
using Zenject;

namespace Gameplay.Services.FX
{
    public class PlayingFX : MonoBehaviour
    {
        private ParticleSystem _particlePrefab;
        private FXContext _context;
        
        [Inject]
        public void Construct(ParticleSystem particlePrefab, FXContext context)
        {
            _particlePrefab = particlePrefab;
            _context = context;
        }

        protected void Start()
        {
            transform.position = _context.Position;

            var particle = Instantiate(_particlePrefab, transform);
            particle.transform.forward = _context.Direction;
            particle.Play();
            
            Destroy(gameObject, particle.main.startLifetime.constant);
        }

        public class Factory : PlaceholderFactory<ParticleSystem, FXContext, PlayingFX>
        {
        }
    }
}
