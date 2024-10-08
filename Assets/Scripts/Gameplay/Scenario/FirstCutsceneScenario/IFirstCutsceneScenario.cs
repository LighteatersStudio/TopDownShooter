using Common.Scenarios;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public interface IFirstCutsceneScenario : IScenario
    {
        public class Factory : PlaceholderFactory<IFirstCutsceneScenario>
        {
        }
    }
}