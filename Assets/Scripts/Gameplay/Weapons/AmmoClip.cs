namespace Gameplay.Weapons
{
    public class AmmoClip
    {
        private int _amount;
        private int _size;
        
        public AmmoClip(int maxBulletAmount)
        {
            _size = maxBulletAmount;
            _amount = maxBulletAmount;
        }
        
        public bool HasAmmo => _amount > 0;

        public bool WasteBullet()
        {
            if (_amount > 0)
            {
                --_amount;
                return true;
            }
        
            return false;
        }

        public void Reload()
        {
            _amount = _size;
        }
    }
}
