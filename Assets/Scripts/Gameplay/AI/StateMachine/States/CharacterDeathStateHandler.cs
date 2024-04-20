﻿using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Gameplay.AI
{
    public class CharacterDeathStateHandler: IAIState
    {
        private readonly CancellationToken _token;
        private readonly Character _character;
        private readonly IdleAIState.Factory _idleFactory;
        
        protected CancellationTokenSource InternalSource;

        protected CharacterDeathStateHandler(CancellationToken token, Character character, IdleAIState.Factory idleFactory)
        {
            _token = token;
            _character = character;
            _idleFactory = idleFactory;
        }

        public virtual async Task<StateResult> Launch()
        {
            HandleCharacterDeath();
            await UniTask.Yield();
            return new StateResult(_idleFactory.Create(_token), true);
        }

        private void HandleCharacterDeath()
        {
            InternalSource = new CancellationTokenSource();

            _token.Register(() => InternalSource.Cancel());

            void HandleDead()
            {
                InternalSource.Cancel();
                _character.Dead -= HandleDead;
            }

            _character.Dead += HandleDead;
        }
    }
}