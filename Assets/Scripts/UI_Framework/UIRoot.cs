using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    internal class UIRoot : MonoBehaviour, IUIRoot
    {
        private UISchema _schema;
        private Layer.Factory _layerFactory;

        private readonly List<Layer> _layer = new();

        [Inject]
        public void Construct(UISchema uiSchema, Layer.Factory layerFactory)
        {
            _schema = uiSchema;
            _layerFactory = layerFactory;

            CreateLayers();
        }

        private void CreateLayers()
        {
            var order = _schema.RenderingOrderOffset - 1;

            foreach (var layerInfo in _schema.Layers)
            {
                var layer = _layerFactory.Create(layerInfo, ++order);
                layer.Init();
                _layer.Add(layer);
            }
        }

        private bool TryGetLayer<TView>(out Layer result) where TView : IView
        {
            result = null;
            foreach (var layer in _layer)
            {
                if (layer.Contains<TView>())
                {
                    result = layer;
                    return true;
                }
            }

            Debug.LogError($"View type[{typeof(TView)}] didn't find in UIScheme!");
            return false;
        }

        public IView Open<TView>() where TView : IView
        {
            if (!TryGetLayer<TView>(out var layer))
            {
                Debug.LogError($"View didn't open!");
                return null;
            }

            return layer.Open<TView>();
        }

        public IView Open<TView, TParam>(TParam param) where TView : IView
        {
            if (!TryGetLayer<TView>(out var layer))
            {
                Debug.LogError($"View didn't open!");
                return null;
            }

            return layer.Open<TView, TParam>(param);
        }

        public IView Open<TView, TParam, TParam2>(TParam param1, TParam2 param2) where TView : IView
        {
            if (!TryGetLayer<TView>(out var layer))
            {
                Debug.LogError($"View didn't open!");
                return null;
            }

            return layer.Open<TView, TParam, TParam2>(param1, param2);
        }

        public IView Open<TView, TParam, TParam2, TParam3>(TParam param1, TParam2 param2, TParam3 param3) where TView : IView
        {
            if (!TryGetLayer<TView>(out var layer))
            {
                Debug.LogError($"View didn't open!");
                return null;
            }

            return layer.Open<TView, TParam, TParam2, TParam3>(param1, param2, param3);
        }
    }
}