using Zenject;
using Services.Pause;

namespace UI
{
    public class StartLevelMenu : PopupBase
    {
        private IPause _pauseManager;

        [Inject]
        public void Construct(IPause pause)
        {
            _pauseManager = pause;
        }
        
        
        private void Start()
        {
            _pauseManager.Paused = true;
        }

        public void ClickGoButton()
        {
            _pauseManager.Paused = false;
            Close();
        }
    }
}