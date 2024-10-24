﻿using System;
using Gameplay.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Installers.Gameplay
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
            Container.Bind<InputActionAsset>()
                .FromScriptableObject(_playerInputActionsMap)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerInput>()
                .FromComponentInNewPrefab(_playerInputPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.Bind(typeof(IUIInputController), typeof(IInitializable), typeof(IDisposable))
                .To<UIInputController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind(typeof(IInputController), typeof(IInitializable), typeof(IDisposable))
                .To<InputController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}