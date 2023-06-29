using Services.Loading;

namespace Meta.Level
{
    public interface ILevelsNavigation
    {
        ILoadingOperation LevelLoading { get; }
        ILoadingOperation MainMenuLoading { get; }
    }
}