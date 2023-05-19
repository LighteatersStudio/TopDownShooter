using UnityEngine;

namespace Gameplay.Weapons
{
    public interface IWeapon
    {
        bool Shot();

        void SetParent(Transform transform);
    }
}
