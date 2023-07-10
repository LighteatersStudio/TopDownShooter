using System;
using UnityEngine;

namespace Services.Coloring
{
    public interface IColorSchema
    {
        Color Main { get; } 
        Color Second { get; } 
        Color Third { get; }

        public class Fake : IColorSchema
        {
            public Color Main => Color.white;
            public Color Second => Color.black;
            public Color Third => Color.red;
        }
    }
}