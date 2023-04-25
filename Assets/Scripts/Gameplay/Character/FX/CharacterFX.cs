using FX;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CharacterFX
    {
        private PlayingFX.Factory _fxFactory;
        private ICharacterFXList _fxList;
        private ICharacter _character;
        private Transform _root;
        
        [Inject]
        public void Construct(PlayingFX.Factory fxFactory, ICharacterFXList fxList, ICharacter character, Transform root)
        {
            _fxFactory = fxFactory;
            _fxList = fxList;
            _character = character;
            _root = root;
            
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
            _fxFactory.Create(_fxList.DeadFx, _root.position);
        }

        private void OnDamaged()
        {
            _fxFactory.Create(_fxList.HitFx, _root.position);
        }
        
        
        public class Factory : PlaceholderFactory<ICharacter,Transform, CharacterFX>
        {
        }
    }
}