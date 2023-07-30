using UnityEngine;

namespace Gameplay.CollectableItems
{
    public abstract class ItemAbstractFactory: MonoBehaviour
    {
        public abstract WeaponCollectable CreateWeapon(WeaponCollectable weapon, IPlayer player);
    }
}