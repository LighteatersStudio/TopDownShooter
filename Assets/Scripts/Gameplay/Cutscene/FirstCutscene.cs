using Zenject;

namespace Gameplay.Cutscene
{
    public class FirstCutscene : Cutscene
    {
        public class Factory : PlaceholderFactory<FirstCutscene>
        {
        }
    }
}