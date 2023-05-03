using Level;
using Zenject;

namespace UI
{
    public class HighStoneChooseMenu : PopupBase
    {
        private GameRunProvider _gameRun;
        
        [Inject]
        public void Construct(GameRunProvider gameRun)
        {
            _gameRun = gameRun;
        }
        
        public void ActivateHighMode()
        {
            _gameRun.Run(GameRunType.High);
            Close();
        }

        public void ActivateStoneMode()
        {
            _gameRun.Run(GameRunType.Stone);
            Close();
        }
    }
}