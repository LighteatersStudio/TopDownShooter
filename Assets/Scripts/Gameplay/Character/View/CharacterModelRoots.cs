using UnityEngine;

namespace Gameplay
{
    public class CharacterModelRoots  : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        
        public Transform Head => _head;
    }
}