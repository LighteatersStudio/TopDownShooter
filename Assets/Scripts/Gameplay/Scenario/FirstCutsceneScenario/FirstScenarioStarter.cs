using UnityEngine;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstScenarioStarter : MonoBehaviour
    {
        private FirstScenarioEvent _firstScenarioEvent;

        [Inject]
        public void Construct(FirstScenarioEvent firstScenarioEvent)
        {
            _firstScenarioEvent = firstScenarioEvent;
        }

        private void Start()
        {
            _firstScenarioEvent.Start();
        }
    }
}