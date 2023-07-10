using Zenject;

namespace Services.Coloring
{
    public class GameColoring : IInitializable 
    {
        public IColorSchemeSettings Settings { get; }
        public IColorSchema Current { get; private set; }
        
        public event System.Action<IColorSchema> Changed;
        
        
        [Inject]
        public GameColoring(IColorSchemeSettings settings)
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