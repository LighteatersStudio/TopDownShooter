using System;

namespace Gameplay.Weapons
{
    public class AmmoClip : IHaveAmmo
    {
        private readonly int _size;
        private int _remainAmmo;

        public bool HasAmmo => _remainAmmo > 0;
        public int RemainAmmo
        {
            get => _remainAmmo;
            private set
            {
                _remainAmmo = value;
                RemainAmmoChanged?.Invoke();
            }
        }

        public event Action RemainAmmoChanged;
        
        
        public AmmoClip(int size)
        {
            _size = size;
            RemainAmmo = size;
        }

        public void WasteBullet()
        {
            if (RemainAmmo > 0)
            {
                --RemainAmmo;
            }
        }

        public void Reload()
        {
            RemainAmmo = _size;
        }
    }
}
