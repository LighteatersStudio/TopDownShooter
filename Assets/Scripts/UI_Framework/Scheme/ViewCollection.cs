using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Framework.Implementation
{
    [Serializable]
    internal  class ViewCollection
    {
        [SerializeField]
        private List<GameObject> _views = new();
        
        public IEnumerable<GameObject> Objects => _views;

        public bool Contains<TView>() where TView : IView
        {
            return _views.Any(x => x.GetComponent<TView>() != null);
        }
        
        public GameObject Get<TView>() where TView : IView
        {
            return _views.Find(x => x.GetComponent<TView>() != null);
        }

        public bool TryGet<TView>(out GameObject view) where TView : IView
        {
            view = Get<TView>();
            return view != null;
        }
    }
}