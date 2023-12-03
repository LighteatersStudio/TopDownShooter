using UnityEngine;

namespace Gameplay
{
    public class CharacterModelRoots  : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _rightHand;
        
        public Transform Head => _head;
        public Transform RightHand => _rightHand;
    }
}