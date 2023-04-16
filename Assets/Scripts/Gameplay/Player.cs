using UnityEngine;

namespace Gameplay
{
    public class Player : MonoBehaviour, IPlayer
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}