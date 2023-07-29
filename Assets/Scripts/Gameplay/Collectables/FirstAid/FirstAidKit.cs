using UnityEngine;

namespace Gameplay.Collectables.FirstAid
{
    [RequireComponent(typeof(RotateCollectables))]
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private float _hpUpAmount;
        
        public void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<Player>();

            if (target == null)
            {
                return;
            }

            var character = target.GetComponentInChildren<Character>();
            character.RecoverHealth(_hpUpAmount);

            Destroy(gameObject);
        }
    }
}