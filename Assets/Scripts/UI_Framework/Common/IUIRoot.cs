namespace UI.Framework
{
    public interface IUIRoot
    {
        TView Open<TView>() where TView : IView;
    }
}