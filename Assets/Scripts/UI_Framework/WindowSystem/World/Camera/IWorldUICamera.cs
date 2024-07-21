using UnityEngine;

namespace UI.Framework.Implementation
{
    public interface IWorldUICamera
    {
        Transform Transform { get; }
        Camera Source { get; }
    }
}