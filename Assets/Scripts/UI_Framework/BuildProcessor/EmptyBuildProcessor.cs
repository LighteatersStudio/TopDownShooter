namespace UI.Framework
{
    internal class EmptyBuildProcessor : IUIBuildProcessor
    {
        public void Process<TView>(TView view) where TView : View
        {
        }
    }
}