using UnityEngine;
using System;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using Services.Utility;
using Zenject;

namespace Gameplay
{
    public class Player : MonoBehaviour, IPlayer, ITicker
    {
        [SerializeField] private WeaponSettings _defaultWeapon;
        
        private DynamicMonoInitializer<IPlayerSettings, Character.Factory, PlayerInputAdapter.Factory, Weapon.Factory> _initializer;
        private Character _character;
        private PlayerInputAdapter _inputAdapter;
        private Weapon.Factory _weaponFactory;
        
        public IWeaponOwner WeaponOwner => _character;

        public event Action Dead;
        public event Action<float> Tick;


        [Inject]
        public void Construct(IPlayerSettings settings, Character.Factory characterFactory,
            PlayerInputAdapter.Factory inputAdapterFactory, Weapon.Factory weaponFactory)
        {
            _initializer =
                new DynamicMonoInitializer<IPlayerSettings, Character.Factory, PlayerInputAdapter.Factory,
                    Weapon.Factory>(
                    settings,
                    characterFactory,
                    inputAdapterFactory,
                    weaponFactory);
        }

        private void Start()
        {
            _initializer.Initialize(Load);
            _character.Dead += OnDead;
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            _character.Dead -= OnDead;
        }

        private void Load(IPlayerSettings settings, Character.Factory characterFactory,
            PlayerInputAdapter.Factory playerInputFactory, Weapon.Factory weaponFactory)
        {
            _character = characterFactory.Create(settings.Stats, parent => Instantiate(settings.Model, parent), TypeGameplayObjects.Player);
            _character.SetParent(transform);

            var moveBehaviour = gameObject.AddComponent<MoveBehaviour>();
            moveBehaviour.SetSpeedHandler(() => _character.MoveSpeed);

            _inputAdapter = playerInputFactory.Create(moveBehaviour, _character, _character, this);
            
            _weaponFactory = weaponFactory;
            ChangeWeapon(_defaultWeapon);
        }

        public void ChangeWeapon(IWeaponSettings settings)
        {
            _character.ChangeWeapon(_weaponFactory.Create(settings, _character));
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