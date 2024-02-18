using Zenject;

namespace Gameplay
{
    public class SelectCharacterModule
    {
        private PlayerSettings _playerSettings;
        private DataConfig _dataConfig;

        [Inject]
        public void Construct(DataConfig dataConfig)
        {
            _dataConfig = dataConfig;
        }
        
        public void SetPlayerSettings(int index)
        {
            _playerSettings = _dataConfig.AllPlayersData.PlayerSettingsArray[index];
        }
        
        public PlayerSettings GetPlayerSettings()
        {
            return _playerSettings;
        }

        public void GetPlayerCharacteristics()
        {
            
        }
    }
}