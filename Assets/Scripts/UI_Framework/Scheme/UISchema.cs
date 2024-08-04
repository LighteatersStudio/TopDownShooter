using System.Collections.Generic;
using UnityEngine;

namespace UI.Framework.Implementation
{
    [CreateAssetMenu(menuName = "UI Framework/Create UISchema", fileName = "UISchema", order = 0)]
    internal  class UISchema : ScriptableObject
    {
        [field: SerializeField] public List<LayerInfo> Layers { get; private set; }
        [field: SerializeField] public int RenderingOrderOffset { get; private set; }
    }
}