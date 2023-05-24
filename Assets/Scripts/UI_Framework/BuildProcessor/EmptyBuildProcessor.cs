namespace UI.Framework
{
    public class EmptyBuildProcessor : IUIBuildProcessor
    {
        public void Process<TView>(TView view) where TView : View
        {
        }
    }
}