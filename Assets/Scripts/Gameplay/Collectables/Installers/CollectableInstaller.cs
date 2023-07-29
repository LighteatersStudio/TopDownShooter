using Gameplay.Collectables.FirstAid;
using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.Installers
{
    public class CollectableInstaller : MonoBehaviour
    {
        private FirstAidKitSpawner.Factory _firstAidKitFactory;
        
        [Inject]
        protected void Construct(FirstAidKitSpawner.Factory firstAidKitFactory)
        {
            _firstAidKitFactory = firstAidKitFactory;
        }

        private void Start()
        {
            _firstAidKitFactory.Create(transform.position);
            Destroy(gameObject);
        }
    }
}