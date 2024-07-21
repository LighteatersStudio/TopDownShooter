using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class WindowSystemSimpleWorld : WindowSystemBase
    {
        [SerializeField] private WorldNodeUI _worldNodeUI;

        private int _order;
        
        public override void SetOrder(int order)
        {
            _order = order;
        }

        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            var node = builder.Build(_worldNodeUI.gameObject, transform).GetComponent<WorldNodeUI>();
            var view = builder.Build<TView>(prefab, node.Root);
            
            node.SetupView(view, _order);
            node.Open();

            return view;
        }
    }
}