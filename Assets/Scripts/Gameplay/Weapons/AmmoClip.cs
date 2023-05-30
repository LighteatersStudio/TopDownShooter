using System;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class AmmoClip : IWeapon
    {
        private int _bulletAmount;
        private int _maxBulletAmount;

        public AmmoClip(int maxBulletAmount)
        {
            _maxBulletAmount = maxBulletAmount;
            _bulletAmount = maxBulletAmount;
        }

        public int MaxBulletAmount
        {
            get
            {
                return _maxBulletAmount;
            }
            set => _maxBulletAmount = value;
        }

        public int CurrentBulletAmount
        {
            get
            {
                return _bulletAmount;
            }
            set
            {
                _bulletAmount = Mathf.Clamp(value, 0, _maxBulletAmount);
            }
        }

        public bool Shot()
        {
            if (_bulletAmount > 0)
            {
                Debug.Log("Shoot");
                --_bulletAmount;
                Debug.Log(_bulletAmount);
                return true;
            }
        
            return false;
        }

        public void Reload()
        {
            _bulletAmount = _maxBulletAmount;
        }

        public bool HasAmmo()
        {
            return _bulletAmount > 0;
        }

        public string WeaponType { get; }
    }
}
