using UnityEngine;
using Zenject;

namespace Gameplay.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private IProjectileMovement _projectileMovement;

        [Inject]
        public void Construct(IProjectileMovement projectileMovement)
        {
            _projectileMovement = projectileMovement;
        }

        public void Launch(int range, float speed)
        {
            _projectileMovement.Move(range, speed);
        }
    }
}

