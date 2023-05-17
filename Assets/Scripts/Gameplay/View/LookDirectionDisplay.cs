using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class LookDirectionDisplay : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LookDirectionDisplay>
        {
        }
    }
}