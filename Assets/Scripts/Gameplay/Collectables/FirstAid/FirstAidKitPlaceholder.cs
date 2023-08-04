using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    public class FirstAidKitPlaceholder : MonoBehaviour
    {
        private FirstAidKit.Factory _firstAidKitFactory;

        [Inject]
        public void Construct(FirstAidKit.Factory firstAidKitFactory)
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