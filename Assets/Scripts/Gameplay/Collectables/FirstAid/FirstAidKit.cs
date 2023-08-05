using UnityEngine;
using Zenject;

namespace Gameplay.Collectables.FirstAid
{
    [RequireComponent(typeof(RotateCollectables))]
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private float _hpUpAmount;

        [Inject]
        public void Construct(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<Player>();
            
            if (target == null)
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