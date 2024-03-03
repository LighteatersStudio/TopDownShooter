using Meta.Level;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HighStoneChooseMenu : Popup
    {
        private GameRunProvider _gameRun;

        [Inject]
        public void Construct(GameRunProvider gameRun)
        {
            _gameRun = gameRun;
        }

        public void ActivateHighMode()
        {
            var parameters = new GameRunParameters(GameRunType.High, 0);
            Debug.LogAssertion("Symbol index not selected and set = 0");
            _gameRun.Run(parameters);
            Close();
        }

        public void ActivateStoneMode()
        {
            var parameters = new GameRunParameters(GameRunType.High, 0);
            Debug.LogAssertion("Symbol index not selected and set = 0");
            _gameRun.Run(parameters);
            Close();
        }
    }
}