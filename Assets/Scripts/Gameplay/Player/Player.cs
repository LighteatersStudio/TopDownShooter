using UnityEngine;
using System;
using Gameplay.Weapons;
using Utility;
using Zenject;

namespace Gameplay
{
    public class Player : MonoBehaviour, IPlayer
    {
        private DynamicMonoInitializer<IPlayerSettings, Character.Factory, PlayerInputAdapter.Factory> _initializer;
        private Character _character;
        private PlayerInputAdapter _inputAdapter;

        public IWeaponOwner WeaponOwner => _character;

        public event Action Dead;


        [Inject]
        public void Construct(IPlayerSettings settings, Character.Factory characterFactory, PlayerInputAdapter.Factory inputAdapterFactory)
        {
            _initializer = new DynamicMonoInitializer<IPlayerSettings, Character.Factory, PlayerInputAdapter.Factory>(
                settings,
                characterFactory,
                inputAdapterFactory);
        }

        private void Start()
        {
            _initializer.Initialize(Load);
            _character.Dead += OnDead;
        }
        
        private void OnDestroy()
        {
            _character.Dead -= OnDead;
        }
        
        private void Load(IPlayerSettings settings, Character.Factory characterFactory, PlayerInputAdapter.Factory playerInputFactory)
        {
            _character = characterFactory.Create(settings.Stats, parent => Instantiate(settings.Model, parent));
            _character.SetParent(transform);

            var moveBehaviour = gameObject.AddComponent<MoveBehaviour>();
            moveBehaviour.SetSpeedHandler(() => _character.MoveSpeed);

            _inputAdapter = playerInputFactory.Create(moveBehaviour, _character, _character);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        private void OnDead()
        {
            Dead?.Invoke();
        }
    }
}