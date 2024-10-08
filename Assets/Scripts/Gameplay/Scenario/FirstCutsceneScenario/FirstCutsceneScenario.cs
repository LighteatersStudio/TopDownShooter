using System.Threading.Tasks;
using Gameplay.Cutscene;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstCutsceneScenario : IFirstCutsceneScenario
    {
        private readonly FirstCutscene.Factory _firstCutsceneFactory;

        [Inject]
        public FirstCutsceneScenario (FirstCutscene.Factory firstCutsceneFactory)
        {
            _firstCutsceneFactory = firstCutsceneFactory;
        }

        public async Task Launch()
        {
            await _firstCutsceneFactory.Create().Play();
        }
    }
}