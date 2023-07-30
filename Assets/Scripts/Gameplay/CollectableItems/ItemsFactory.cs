using Zenject;

namespace Gameplay.CollectableItems
{
    public class ItemsFactory: ItemAbstractFactory
    {
        public override WeaponCollectable CreateWeapon(WeaponCollectable currentWeapon, IPlayer player)
        {
            var weapon = Instantiate(currentWeapon);
            weapon.transform.position = transform.position;
            weapon.Construct(player);
            
            return weapon;
        }
        
        public class Factory : PlaceholderFactory<ItemsFactory>
        {
        }
    }
}