using Meta.Level;
using Services.Loading;

namespace Infrastructure.Loading
{
    public class LevelsNavigation : ILevelsNavigation
    {
        public ILoadingOperation LevelLoading { get; }
        public ILoadingOperation MainMenuLoading { get; }

        public LevelsNavigation(LevelLoadingOperation levelLoading, MainMenuLoadingOperation menuLoading)
        {
            LevelLoading = levelLoading;
            MainMenuLoading = MainMenuLoading;
        }
    }
}