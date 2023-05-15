using UnityEngine;

namespace Gameplay.Projectile
{
    public interface IProjectileMovement
    {
        public void Move(Vector3 position, Vector3 direction);
    }
}