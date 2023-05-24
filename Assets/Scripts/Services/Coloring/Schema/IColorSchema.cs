using System;
using UnityEngine;

namespace Services.AppVersion.Coloring
{
    public interface IColorSchema
    {
        Color Main { get; } 
        Color Second { get; } 
        Color Third { get; }
    }
}