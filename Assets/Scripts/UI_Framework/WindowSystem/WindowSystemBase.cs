using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    public abstract class WindowSystemBase : MonoBehaviour, IWindowSystem
    {
        private UIBuilder.Factory BuilderFactory { get; set; }
        
        private UIBuilderInstaller _builderInstaller;

        [Inject]
        public void Construct(UIBuilder.Factory uiBuilder, UIBuilderInstaller builderInstaller)
        {
            BuilderFactory = uiBuilder;
            _builderInstaller = builderInstaller;
        }


        public void Open(GameObject prefab)
        {
            Instantiate(prefab, transform);
        }

        public TView Open<TView>(GameObject prefab) where TView : IView
        {
            return Open<TView>(BuilderFactory.Create(_builderInstaller.CreateContext()), prefab);
        }

        public TView Open<TView, TParam>(GameObject prefab, TParam param) where TView : IView
        {
            return Open<TView>(BuilderFactory.Create(_builderInstaller.CreateContext(param)), prefab); 
        }

        public TView Open<TView, TParam, TParam2>(GameObject prefab, TParam param1, TParam2 param2) where TView : IView
        {
            return Open<TView>(BuilderFactory.Create(_builderInstaller.CreateContext(param1, param2)), prefab);
        }

        public TView Open<TView, TParam, TParam2, TParam3>(GameObject prefab, TParam param1, TParam2 param2, TParam3 param3) where TView : IView
        {
            return Open<TView>(BuilderFactory.Create(_builderInstaller.CreateContext(param1, param2, param3)), prefab);
        }

        public virtual void SetName(string objName)
        {
            gameObject.name = $"{objName} - '{gameObject.name.Replace("(Clone)",string.Empty)}'";
        }

        public abstract void SetOrder(int order);
        
        protected abstract TView Open<TView>(UIBuilder builder, GameObject prefab) where TView : IView;
    }
}