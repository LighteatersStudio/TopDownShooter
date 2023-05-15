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
                var projectile = Instantiate(_projectilePrefab);
               
                projectile.Launch(Vector3.up,50, TypeDamage.Fire);
            }
        }
    }
}