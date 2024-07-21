using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class WindowSystemSingleWorld : WindowSystemSimpleWorld
    {
        private IView _current;

        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            _current?.Close();
            
            var view = base.Open<TView>(builder, prefab);
            view.Closed += OnViewClosed;
            
            _current = view;
            
            return view;
        }

        private void OnViewClosed(IView view)
        {
            view.Closed -= OnViewClosed;
            _current = null;
        }
    }
}