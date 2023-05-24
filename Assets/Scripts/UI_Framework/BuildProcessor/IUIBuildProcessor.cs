namespace UI.Framework
{
    public interface IUIBuildProcessor
    {
        void Process<TView>(TView view) where TView : View;
    }
}