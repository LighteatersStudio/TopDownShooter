using UnityEngine;

namespace Gameplay
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; }
    }
}