using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponModelRoots : MonoBehaviour
    {
        [SerializeField] private Transform _muzzle;

        public Transform Muzzle => _muzzle;
    }
}