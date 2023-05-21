using Zenject;

namespace Coloring
{
    public class GameColoring : IInitializable 
    {
        public ColorSchemeSettings Settings { get; }
        public IColorSchema Current { get; private set; }
        
        public event System.Action<IColorSchema> Changed;
        
        
        [Inject]
        public GameColoring(ColorSchemeSettings settings)
        {
            Settings = settings;
        }

        public void Initialize()
        {
            SwitchTo(Settings.Default);
        }
        
        public void SwitchTo(IColorSchema schema)
        {
            Current = schema;
            
            Changed?.Invoke(Current);
        }
    }
}