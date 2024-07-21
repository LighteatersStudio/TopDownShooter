using Zenject;

namespace UI.Framework
{
    public class ViewFactory
    {
        [Inject]
        protected readonly IUIRoot UIRoot;
    } 
    
    public class ViewFactory<TView> : ViewFactory where TView : IView
    {
        public IView Open()
        {
            return UIRoot.Open<TView>();
        }
    }
    
    public class ViewFactory<TView, TParam> : ViewFactory where TView : IView
    {
        public IView Open(TParam param)
        {
            return UIRoot.Open<TView, TParam>(param);
        }
    }
    
    public class ViewFactory<TView, TParam, TParam2> : ViewFactory where TView : IView
    {
        public IView Open(TParam param, TParam2 param2)
        {
            return UIRoot.Open<TView, TParam, TParam2>(param, param2);
        }
    }
    
    public class ViewFactory<TView, TParam, TParam2, TParam3> : ViewFactory where TView : IView
    {
        public IView Open(TParam param1, TParam2 param2, TParam3 param3)
        {
            return UIRoot.Open<TView, TParam, TParam2, TParam3>(param1, param2, param3);
        }
    }
}