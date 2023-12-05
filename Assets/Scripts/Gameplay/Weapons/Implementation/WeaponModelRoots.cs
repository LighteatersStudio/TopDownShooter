using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponModelRoots : MonoBehaviour
    {
        [field: SerializeField] public Transform Muzzle { get; private set; }
    }
}