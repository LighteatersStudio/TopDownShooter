using System;

namespace Gameplay.Weapons
{
    public class AmmoClip : IHaveAmmo
    {
        private int _amount;
        private int _size;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                AmountChanged?.Invoke();
            }
        }
        
        public event Action AmountChanged;
        
        public AmmoClip(int maxBulletAmount)
        {
            _size = maxBulletAmount;
            Amount = maxBulletAmount;
        }
        
        public bool HasAmmo => _amount > 0;

        public bool WasteBullet()
        {
            if (_amount > 0)
            {
                --Amount;
                return true;
            }
        
            return false;
        }

        public void Reload()
        {
            Amount = _size;
        }
    }
}
