using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    public class FirstAidKitSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _kits;

        private Vector3 _newPosition;

        [Inject]
        protected void Construct(Vector3 newPosition)
        {
            _newPosition = newPosition;
        }

        private void Start()
        {
            Instantiate(_kits[Random.Range(0, _kits.Length - 1)], _newPosition, transform.rotation);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Vector3, FirstAidKitSpawner>
        {
        }
    }
}