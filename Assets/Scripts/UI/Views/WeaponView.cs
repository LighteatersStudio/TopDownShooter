using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Image _weapon;

        private Image _currentWeapon;

        public void IncludeWeapon()
        {
           _currentWeapon = Instantiate(_weapon, transform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (_currentWeapon != null)
                {
                    Destroy(_currentWeapon.gameObject);
                }

                IncludeWeapon();
            }
        }
    }
}