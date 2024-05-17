using System;
using UnityEngine;

namespace UI.Framework.Implementation
{
    [Serializable]
    internal  class LayerInfo
    {
        [field: SerializeField] public WindowSystemBase WindowSystem { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public ViewCollection ViewCollection { get; private set; }
        [field: SerializeField] public bool Lazy { get; private set; } = false;
        [field: SerializeField] public bool Debug { get; private set; } = false;

        public override string ToString()
        {
            return Name;
        }
    }
}