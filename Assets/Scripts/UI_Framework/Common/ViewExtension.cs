namespace UI.Framework
{
    public static class ViewExtension
    {
        public static bool Avaliable(this IView view)
        {
            return (view != null) && view.IsAlive;
        }

        public static IView SafetyGetInstance(this IView view)
        {
            if (view.Avaliable())
            {
                return view;
            }

            return null;
        }
    }
}