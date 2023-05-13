using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Projectile
{
    public class TestShooting : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);

                var movementArc = projectile.AddComponent<MovementArc>();
                projectile.Construct(movementArc);
                
                projectile.Launch(10,5.1f);
            }
        }
    }
}