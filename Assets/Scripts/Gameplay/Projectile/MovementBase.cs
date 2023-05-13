using UnityEngine;

namespace Gameplay.Projectile
{
    public abstract class MovementBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<Player>() && !other.GetComponent<Projectile>())
            {
                Destroy(gameObject);
            }
        }
    }
}