using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class SimpleWindowSystem : CanvasWindowSystemBase
    {
        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            var view = builder.Build<TView>(prefab, transform);
            
            view.Closed += OnViewClosed;
            view.Open();

            return view;
        }

        protected virtual void OnViewClosed(IView view)
        {
            view.Closed -= OnViewClosed;
        }
    }
}