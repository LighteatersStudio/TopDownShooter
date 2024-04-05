using UnityEngine;

namespace Gameplay
{
    public interface IMovable
    {
        void SetMoveForce(Vector3 direction, float force = 1);
        void Stop();
    }
}