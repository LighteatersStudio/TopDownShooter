using UnityEngine;

namespace Gameplay.Projectiles
{
    public class TestShooting : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var projectile = Instantiate(_projectilePrefab);
            }
        }
    }
}