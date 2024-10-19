using System.Threading.Tasks;
using Services.Utilities;
using UnityEngine;

namespace Gameplay.Scenario
{
    public class ScenarioPlayer
    {
        private readonly TaskProcessQueue _taskQueue;

        public ScenarioPlayer()
        {
            _taskQueue = new TaskProcessQueue();
        }

        public Task Play(IScenario scenario)
        {
            Debug.Log("Scenario added to queue: " + scenario);

            return _taskQueue.Start(() => PlayInternal(scenario));
        }

        private async Task PlayInternal(IScenario scenario)
        {
            Debug.Log("Scenario play: " + scenario);

            await _taskQueue.Start(scenario.Launch);

            Debug.Log("Scenario finished: " + scenario);
        }
    }
}