namespace Services.Coloring
{
    public interface IColorSchemeSettings
    {
        IColorSchema Default { get; }
        IColorSchema High { get; }
        IColorSchema Stone { get; }
        
        public class Fake : IColorSchemeSettings
        {
            public IColorSchema Default { get; }
            public IColorSchema High { get; }
            public IColorSchema Stone { get; }
        }
    }
}