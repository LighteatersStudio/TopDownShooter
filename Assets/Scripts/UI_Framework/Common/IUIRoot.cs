namespace UI.Framework
{
    public interface IUIRoot
    {
        IView Open<TView>() where TView : IView;
        IView Open<TView, TParam>(TParam param) where TView : IView;
        IView Open<TView, TParam, TParam2>(TParam param1, TParam2 param2) where TView : IView;
        IView Open<TView, TParam, TParam2, TParam3>(TParam param1, TParam2 param2, TParam3 param3) where TView : IView;
    }
}