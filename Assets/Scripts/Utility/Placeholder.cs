using UnityEngine;
using Zenject;

namespace Utility
{
    public abstract class Placeholder : MonoBehaviour
    {
        private void Start()
        {
            var transformInstance = Create().transform;
            
            transformInstance.SetParent(transform);
            transformInstance.SetZeroPositionRotation();
            
            Destroy(this);
        }

        protected abstract GameObject Create();
    }
    
    public abstract class Placeholder<TFactory> : Placeholder
    {
        private TFactory _factory;
        
        [Inject]
        public void Construct(TFactory factory)
        {
            _factory = factory;
        }

        protected override GameObject Create()
        {
            return Create(_factory);
        }

        protected abstract GameObject Create(TFactory factory);

    }
}