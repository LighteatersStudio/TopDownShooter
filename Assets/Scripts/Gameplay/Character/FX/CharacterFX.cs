﻿using Gameplay.Services.FX;
using Zenject;

namespace Gameplay
{
    public class CharacterFX
    {
        private PlayingFX.Factory _fxFactory;
        private ICharacterFXList _fxList;
        private ICharacter _character;
        
        [Inject]
        public void Construct(PlayingFX.Factory fxFactory, ICharacterFXList fxList, ICharacter character)
        {
            _fxFactory = fxFactory;
            _fxList = fxList;
            _character = character;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _character.Damaged += OnDamaged;
            _character.Dead += OnDead;
        }
        
        private void Unsubscribe()
        {
            _character.Damaged -= OnDamaged;
            _character.Dead -= OnDead;
        }

        private void OnDead()
        {
            Unsubscribe();
            _fxFactory.Create(_fxList.DeadFx, new FXContext(_character.ModelRoots.Head.position, _character.ModelRoots.Head.forward));
        }

        private void OnDamaged()
        {
            _fxFactory.Create(_fxList.HitFx, new FXContext(_character.ModelRoots.Head.position, _character.ModelRoots.Head.forward));
        }
    }
}