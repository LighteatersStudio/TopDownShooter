using UnityEngine;

namespace Gameplay
{
    public class CharacterModelRoots  : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _weapon;
        [SerializeField] private Transform _projectileRoot;
        
        public Transform Head => _head;
        public Transform Weapon => _weapon;
        public Transform ProjectileRoot => _projectileRoot;
    }
}