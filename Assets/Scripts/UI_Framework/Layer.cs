using System;
using Zenject;

namespace UI.Framework.Implementation
{
    internal class Layer
    {
        private readonly WindowSystemFactory _windowSystemFactory;
        private readonly WindowSystemBase _windowSystemPrefab;
        
        private readonly ViewCollection _collection;
        private readonly string _name;
        private readonly int  _order;
        private readonly bool  _lazy;
        private readonly bool  _debug;
        
        private IWindowSystem _windowSystem;
        
        public Layer(LayerInfo layerInfo, int order, WindowSystemFactory windowSystemFactory)
        {
            _collection = layerInfo.ViewCollection;
            _windowSystemPrefab = layerInfo.WindowSystem;
            _name = layerInfo.Name;
            _order = order;
            _lazy = layerInfo.Lazy;
            _debug = layerInfo.Debug;

            _windowSystemFactory = windowSystemFactory;
        }

        public void Init()
        {
            if (_debug || !_lazy)
            {
                _windowSystem = _windowSystemFactory.Create(_windowSystemPrefab.gameObject, _name, _order);    
            }
             
            if (_debug)
            {
                foreach (var rawView in _collection.Objects)
                {
                    _windowSystem.Open(rawView);
                }
            }
        }

        public bool Contains<TView>() where TView : IView
        {
            return _collection.Contains<TView>();
        }
        
        public TView Open<TView>() where TView : IView
        {
            return OpenInternal(() => _windowSystem.Open<TView>(_collection.Get<TView>()));
        }
        
        public TView Open<TView, TParam>(TParam param) where TView : IView
        {
            return OpenInternal(() => _windowSystem.Open<TView, TParam>(_collection.Get<TView>(), param));
        }
        
        public TView Open<TView, TParam, TParam2>(TParam param1, TParam2 param2) where TView : IView
        {
            return OpenInternal(() => _windowSystem.Open<TView, TParam, TParam2>(_collection.Get<TView>(), param1, param2));
        }
        
        public TView Open<TView, TParam, TParam2, TParam3>(TParam param1, TParam2 param2, TParam3 param3) where TView : IView
        {
            return OpenInternal(() => _windowSystem.Open<TView, TParam, TParam2, TParam3>(_collection.Get<TView>(), param1, param2, param3));
        }
        
        private TView OpenInternal<TView>(Func<TView> creation) where TView : IView
        {
            if (_debug)
            {
                return default;
            }
            
            _windowSystem ??= _windowSystemFactory.Create(_windowSystemPrefab.gameObject, _name, _order);
            
            return creation.Invoke();
        }
        
        
        public class Factory : PlaceholderFactory<LayerInfo, int,  Layer>
        {
        }
    }
}