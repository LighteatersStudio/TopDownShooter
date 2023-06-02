using UnityEngine;

namespace Gameplay
{
    public interface ICanFire
    {
        Vector3 LookDirection { get; set; }
        void Fire();
    }
}