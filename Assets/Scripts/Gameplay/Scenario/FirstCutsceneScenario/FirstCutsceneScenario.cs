using System.Threading.Tasks;
using Gameplay.Cutscene;
using Gameplay.Services.Input;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstCutsceneScenario : IFirstCutsceneScenario
    {
        private readonly FirstCutscene.Factory _firstCutsceneFactory;
        private readonly IInputController _inputService;

        [Inject]
        public FirstCutsceneScenario (FirstCutscene.Factory firstCutsceneFactory, IInputController inputService)
        {
            _firstCutsceneFactory = firstCutsceneFactory;
            _inputService = inputService;
        }

        public async Task Launch()
        {
            using (_inputService.Lock())
            {
                await _firstCutsceneFactory.Create().Play();
            }

        }
    }
}