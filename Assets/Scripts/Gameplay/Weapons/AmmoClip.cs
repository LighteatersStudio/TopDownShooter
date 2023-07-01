using System;
using Gameplay.Services.GameTime;

namespace Gameplay.Weapons
{
    public class AmmoClip : IHaveAmmo
    {
        private readonly int _size;
        private readonly float  _reloadTime;
        
        private int _remainAmmo;
        private ICooldown _reloadCooldown = Cooldown.NewFinished();
        
        public bool HasAmmo => _remainAmmo > 0;
        public bool Reloading => !_reloadCooldown.IsFinish;
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
        public event Action<ICooldown> Reloaded;
        
        
        public AmmoClip(int size, float reloadTime)
        {
            _size = size;
            _reloadTime = reloadTime;
            RemainAmmo = size;
        }
        
        public void WasteBullet()
        {
            if (RemainAmmo > 0)
            {
                --RemainAmmo;
            }
        }

        public ICooldown Reload(ITicker ticker)
        {
            RemainAmmo = 0;
            
            _reloadCooldown = new Cooldown(_reloadTime, ticker, FinishReload);
            Reloaded?.Invoke(_reloadCooldown);
            return _reloadCooldown;
        }

        private void FinishReload()
        {
            RemainAmmo = _size;
        }
            
    }
}
