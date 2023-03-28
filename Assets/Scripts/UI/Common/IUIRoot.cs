namespace UI
{
    public interface IUIRoot
    {
        TView Open<TView>() where TView : IView;
    }
}