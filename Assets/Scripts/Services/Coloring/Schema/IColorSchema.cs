using System;
using UnityEngine;

namespace Services.Coloring
{
    public interface IColorSchema
    {
        Color Main { get; } 
        Color Second { get; } 
        Color Third { get; }
    }
}