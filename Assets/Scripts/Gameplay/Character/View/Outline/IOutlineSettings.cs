using UnityEngine;

namespace Gameplay
{
    public interface IOutlineSettings
    {
        Color Player { get; }
        Color Enemy { get; }
        Color Neutral { get; }
    }
}