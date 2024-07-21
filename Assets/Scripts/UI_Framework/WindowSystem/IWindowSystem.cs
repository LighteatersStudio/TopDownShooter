using UnityEngine;

namespace UI.Framework.Implementation
{
    internal interface IWindowSystem
    {
        void Open(GameObject prefab);
        
        TView Open<TView>(GameObject prefab) where TView : IView;
        TView Open<TView, TParam>(GameObject prefab, TParam param) where TView : IView;
        TView Open<TView, TParam, TParam2>(GameObject prefab, TParam param1, TParam2 param2) where TView : IView;
        TView Open<TView, TParam, TParam2, TParam3>(GameObject prefab, TParam param1, TParam2 param2, TParam3 param3) where TView : IView;
        
        void SetName(string objName);

        void SetOrder(int order);
    }
}