using System;
using Gameplay.Services.GameTime;
using Zenject;

namespace Gameplay
{
    public class GameStateManager : IGameState, IInitializable
    {
        private readonly IPlayer _player;
        private readonly IGameTime _gameTime;
        private readonly LevelSettings _levelSettings;
        public event Action Won;
        public event Action PlayerDead;


        public GameStateManager(IPlayer player, IGameTime gameTime, LevelSettings levelSettings)
        {
            _player = player;
            _gameTime = gameTime;
            _levelSettings = levelSettings;
        }

        public void Initialize()
        {
            _player.Dead += Death;
            _gameTime.Finished += OnTimeFinished;
            _gameTime.Start(_levelSettings.LevelDurationS);
        }

        void IGameState.Win()
        {
            Release();
            Won?.Invoke();
        }

        public void Death()
        {
            Release();
            PlayerDead?.Invoke();
        }

        private void OnTimeFinished()
        {
            ((IGameState)this).Win();
        }

        private void Release()
        {
            _player.Dead -= Death;
            _gameTime.Finished -= OnTimeFinished;
            _gameTime.Break();
        }


    }
}