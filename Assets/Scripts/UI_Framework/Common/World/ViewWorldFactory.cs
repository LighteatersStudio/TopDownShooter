namespace UI.Framework
{
    public class ViewWorldFactory<TView> : ViewFactory<TView, IViewTarget> where TView : IView
    {
    }

    public class ViewWorldFactory<TView, TParam> : ViewFactory<TView, IViewTarget, TParam> where TView : IView
    {
    }
    
    public class ViewWorldFactory<TView, TParam1, TParam2> : ViewFactory<TView, IViewTarget, TParam1, TParam2> where TView : IView
    {
    }
}