using UnityEngine;

namespace Gameplay.Projectiles
{
    public interface IProjectileMovement
    {
        public void Move(Vector3 position, Vector3 direction);
    }
}