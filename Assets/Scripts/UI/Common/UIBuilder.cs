using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIBuilder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _viewsPrefab;
        
        private DiContainer _container;
        private readonly Dictionary<Type, List<IView>> _pool = new();
        
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public TView Build<TView>(Transform parent) where TView : IView
        {
            var viewPrefab = _viewsPrefab.Find(x => x.GetComponent<TView>() != null);

            if (viewPrefab == null)
            {
                Debug.LogError($"View{typeof(TView)} not found. Builder: {name}");
                return default;
            }

            return _container.InstantiatePrefab(viewPrefab, parent).GetComponent<TView>();   
        }

        public void ToPool<TView>(TView view) where TView : IView
        {
            if(_pool.TryGetValue(typeof(TView), out var list))
            {
                list.Add(view);
                return; 
            }

            _pool.Add(typeof(TView), new List<IView> {view});
        }
    }
}