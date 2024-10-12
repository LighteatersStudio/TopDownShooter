using Zenject;

namespace Gameplay.Scenario.Cutscene
{
    public class FirstCutscene : Cutscene
    {
        public class Factory : PlaceholderFactory<FirstCutscene>
        {
        }
    }
}