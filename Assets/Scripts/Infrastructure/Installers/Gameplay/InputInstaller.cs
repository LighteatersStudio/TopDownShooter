using Gameplay.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure
{
    public class InputInstaller : MonoInstaller
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset _playerInputActionsMap;
        [SerializeField] private PlayerInput _playerInputPrefab;
        
        public override void InstallBindings()
        {
            BindInput();
        }
        
        private void BindInput()
        {
            Debug.Log("Game installer: Bind UI input controller");
            
            Container.Bind<InputActionAsset>()
                .FromScriptableObject(_playerInputActionsMap)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerInput>()
                .FromComponentInNewPrefab(_playerInputPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIInputController>()
                .To<UIInputController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IInputController>()
                .To<InputController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
