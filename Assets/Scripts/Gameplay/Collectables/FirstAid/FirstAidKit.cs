using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    [RequireComponent(typeof(RotateCollectables))]
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private float _hpUpAmount;

        private IFirstAidKitSettings _firstAidKitSettings;
        private float _timer;

        [Inject]
        public void Construct(Vector3 newPosition, IFirstAidKitSettings firstAidKitSettings)
        {
            transform.position = newPosition;
            _firstAidKitSettings = firstAidKitSettings;
        }

        private void Update()
        {
            if (_timer > _firstAidKitSettings.LifeTime)
            {
                Destroy(gameObject);
            }

            _timer += Time.deltaTime;
        }

        public void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<Player>();

            if (target == null)
            {
                return;
            }

            if (target.Health.HealthRelative.Equals(1f))
            {
                return;
            }

            var character = target.GetComponentInChildren<Character>();
            character.RecoverHealth(_hpUpAmount);

            GetComponent<RotateCollectables>().StopRotation();

            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Vector3, FirstAidKit>
        {
        }
    }
}