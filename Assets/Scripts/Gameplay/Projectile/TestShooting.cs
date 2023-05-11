using UnityEngine;

namespace Gameplay.Projectile
{
    public class TestShooting : MonoBehaviour
    {
        [SerializeField] private ProjectileMovement _projectilePrefab;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
                projectile.Launch(10,2.1f, TrajectoryType.InArc);
            }
        }
    }
}