using System;
using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class SimpleWindowSystem : CanvasWindowSystemBase
    {
        public override event Action<IWindowsSystemController> ViewOpen;
        public override event Action<IWindowsSystemController> ViewClosed;

        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            var view = builder.Build<TView>(prefab, transform);

            view.Closed += OnViewClosed;
            view.Open();
            ViewOpen?.Invoke(this);

            return view;
        }

        protected virtual void OnViewClosed(IView view)
        {
            view.Closed -= OnViewClosed;
            ViewClosed?.Invoke(this);
        }
    }
}