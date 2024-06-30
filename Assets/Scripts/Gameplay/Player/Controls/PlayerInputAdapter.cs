﻿using System;
using Gameplay.Services.GameTime;
using Gameplay.Services.Input;
using Gameplay.Services.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInputAdapter : IInitializable, IDisposable
    {
        private readonly IMovable _movingActor;
        private readonly IInputController _inputController;
        private readonly ICanFire _fireActor;
        private readonly IPause _pause;
        private readonly ICanReload _reloadActor;
        private readonly ITicker _ticker;

        private bool _isMoving;
        private bool _isLooking;

        private Vector3 _moveDirection;

        [Inject]
        public PlayerInputAdapter(
            IInputController inputController,
            IMovable movingActor,
            ICanFire fireActor,
            ICanReload reloadActor,
            IPause pause,
            ITicker ticker)
        {
            _fireActor = fireActor;
            _inputController = inputController;
            _movingActor = movingActor;
            _pause = pause;
            _reloadActor = reloadActor;
            _ticker = ticker;
        }

        public void Initialize()
        {
            _inputController.MoveChanged += OnMoveChanged;
            _inputController.LookChanged += OnLookChanged;

            _inputController.FireChanged += OnFireChanged;
            _inputController.ReloadChanged += OnReloadChanged;

            _inputController.FingerDown += OnFingerDown;
            _inputController.FingerMoved += OnFingerMoved;
            _inputController.FingerUp += OnFingerUp;
        }

        private void OnFingerDown(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            _isMoving = isMoving;
            _isLooking = isLooking;
        }

        private void OnFingerMoved(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            _isMoving = isMoving;
            _isLooking = isLooking;
        }

        private void OnFingerUp(bool isMoving, bool isLooking)
        {
            _isMoving = isMoving;
            _isLooking = isLooking;
        }

        private void OnMoveChanged(Vector2 direction)
        {
            if (!_isMoving)
            {
                return;
            }

            _moveDirection = new Vector3(direction.x, 0, direction.y);

            _pause.TryInvokeIfNotPause(() => _movingActor.SetMoveForce(_moveDirection));
            _ticker.Tick -= RepeatMove;
            _ticker.Tick += RepeatMove;
        }

        private void RepeatMove(float deltaTime)
        {
            if (!_isMoving)
            {
                _ticker.Tick -= RepeatMove;
                return;
            }

            _pause.TryInvokeIfNotPause(() => _movingActor.SetMoveForce(_moveDirection));
        }

        private void OnLookChanged(Vector2 direction)
        {
            if (!_isLooking)
            {
                return;
            }

            _pause.TryInvokeIfNotPause(() => _fireActor.LookDirection = new Vector3(direction.x, 0, direction.y));
        }

        private void OnFireChanged(bool isActive)
        {
            if (isActive)
            {
                _ticker.Tick -= RepeatAttack;
                _ticker.Tick += RepeatAttack;
                RepeatAttack(0);
            }
            else
            {
                _ticker.Tick -= RepeatAttack;
            }
        }

        private void OnReloadChanged()
        {
            _pause.TryInvokeIfNotPause(() => _reloadActor.Reload());
        }

        private void RepeatAttack(float deltaTime)
        {
            _pause.TryInvokeIfNotPause(() => _fireActor.Fire());
        }

        public void Dispose()
        {
            _inputController.MoveChanged -= OnMoveChanged;
            _inputController.LookChanged -= OnLookChanged;

            _inputController.FireChanged -= OnFireChanged;
            _inputController.ReloadChanged -= OnReloadChanged;

            _inputController.FingerDown -= OnFingerDown;
            _inputController.FingerMoved -= OnFingerMoved;
            _inputController.FingerUp -= OnFingerUp;

            _ticker.Tick -= RepeatAttack;
            _ticker.Tick -= RepeatMove;
        }
    }
}