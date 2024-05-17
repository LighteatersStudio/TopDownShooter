using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class SingleWindowSystem : SimpleWindowSystem
    {
        private IView _current;
        
        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            _current?.Close();
            
            var view = base.Open<TView>(builder, prefab);
            _current = view;
            view.Open();
            
            return view;
        }

        protected override void OnViewClosed(IView view)
        {
            _current = null;
            base.OnViewClosed(view);
        }
    }
}