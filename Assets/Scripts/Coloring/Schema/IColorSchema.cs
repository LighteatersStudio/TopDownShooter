using System;
using UnityEngine;

namespace Coloring
{
    public interface IColorSchema
    {
        Color Main { get; } 
        Color Second { get; } 
        Color Third { get; }
    }
}