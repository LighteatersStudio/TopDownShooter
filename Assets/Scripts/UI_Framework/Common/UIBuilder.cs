using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI.Framework
{
    public class UIBuilder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _viewsPrefab;
        
        private DiContainer _container;
        private readonly List<IView> _pool = new();
        
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public TView Build<TView>(Transform parent) where TView : IView
        {
            if (FromPool(out TView view))
            {   
                return view;
            }

            var viewPrefab = _viewsPrefab.Find(x => x.GetComponent<TView>() != null);

            if (viewPrefab == null)
            {
                Debug.LogError($"View{typeof(TView)} not found. Builder: {name}");
                return default;
            }

            return _container.InstantiatePrefab(viewPrefab, parent).GetComponent<TView>();   
        }

        private bool FromPool<TView>(out TView view) where TView : IView
        {
            const int unfundedInPoolIndex = -1;
            
            var index = _pool.FindIndex(x => x is TView);
            
            if(index != unfundedInPoolIndex)
            {
                view = (TView) _pool[index];
                _pool.RemoveAt(index);
                return true;
            }
            
            view = default;
            return false;
        }   

        public void ToPool<TView>(TView view) where TView : IView
        {
            _pool.Add(view);
        }
    }
}