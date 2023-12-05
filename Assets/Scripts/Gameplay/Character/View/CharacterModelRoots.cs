using UnityEngine;

namespace Gameplay
{
    public class CharacterModelRoots : MonoBehaviour
    {
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform Weapon { get; private set; }
        [field: SerializeField] public Transform Projectile { get; private set; }
    }
}