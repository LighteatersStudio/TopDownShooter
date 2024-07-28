using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class IdleAIState : StateBase
    {
        private readonly CancellationToken _token;
       // private readonly PatrolAIState.Factory _patrolFactory;

        public IdleAIState(CancellationToken token, /*PatrolAIState.Factory patrolFactory,*/ DeathTransition.Factory deathTransition)
            : base(token, new []{deathTransition})
        {
            _token = token;
           // _patrolFactory = patrolFactory;
        }

        protected override void BeginInternal()
        {
        }

        protected override async Task<IAIState> LaunchInternal(CancellationToken token)
        {
           
            /*return _patrolFactory.Create(_token);*/

            try
            {
                await Task.Delay(100000, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return null;
        }

        protected override void EndInternal()
        {
        }

        public class Factory : PlaceholderFactory<CancellationToken, IdleAIState>
        {
        }
    }
}