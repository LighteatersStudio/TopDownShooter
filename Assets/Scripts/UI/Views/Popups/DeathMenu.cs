using Zenject;
using Meta.Level;
using Gameplay.Services.Pause;
using Gameplay.Services.GameTime;
using UI.Framework;

namespace UI
{
    public class DeathMenu : Popup
    {
        private IPause _pauseManager;
        private IGameRun _gameRun;
        private IGameTime _gameTime;

        [Inject]
        public void Construct(IPause pause, IGameRun gameRun, IGameTime gameTime)
        {
            _pauseManager = pause;
            _gameRun = gameRun;
            _gameTime = gameTime;
        }

        private void Start()
        {
            _pauseManager.Paused = true;
        }

        public void PressDoneButton()
        {
            _pauseManager.Paused = false;
            Close();
            _gameRun.Finish();
        }

        public class Factory : ViewFactory<DeathMenu>
        {
        }
    }
}