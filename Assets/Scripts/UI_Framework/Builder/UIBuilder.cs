using UnityEngine;
using Zenject;

namespace UI.Framework
{
    public class UIBuilder
    {
        private readonly ViewCreator _creator;
        private readonly IUIBuildProcessor _buildProcessor;

        public UIBuilder(ViewCreator creator, IUIBuildProcessor buildProcessor)
        {
            _creator = creator;
            _buildProcessor = buildProcessor;
        }

        public GameObject Build(GameObject prefab, Transform parent)
        {
            return _creator.Create(prefab, parent);
        }
        
        public TView Build<TView>(GameObject prefab, Transform parent) where TView : IView
        {
            TView view = _creator.Create(prefab, parent).GetComponent<TView>();
            InvokeBuildProcessor(view);
            
            return view;
        }
        
        private void InvokeBuildProcessor<TView>(TView newView) where TView : IView
        {
            if (newView is View view)
            {
                _buildProcessor.Process(view);
            }
        }

        public class Factory : PlaceholderFactory<ViewCreator, UIBuilder>
        {
        }
    }
}