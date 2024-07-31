// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
// using Zenject;
//
// namespace Gameplay.AI
// {
//     public class AttackingAIState : BaseState
//     {
//         private readonly CancellationToken _token;
//         private readonly Character _character;
//         private readonly NavMeshMoving _moving;
//         private readonly ObserveArea _observeArea;
//         private readonly PursueTargetAIState.Factory _pursueTargetAIFactory;
//         private readonly DeathAIState.Factory _deathAIFactory;
//
//         public AttackingAIState(CancellationToken token, Character character,
//             NavMeshMoving moving, ObserveArea observeArea,
//             PursueTargetAIState.Factory pursueTargetAIFactory, IdleAIState.Factory idleFactory,
//             DeathAIState.Factory deathAIFactory) : base(token, character, idleFactory)
//         {
//             _token = token;
//             _character = character;
//             _moving = moving;
//             _observeArea = observeArea;
//             _pursueTargetAIFactory = pursueTargetAIFactory;
//             _deathAIFactory = deathAIFactory;
//         }
//
//         public override async Task<StateResult> Launch()
//         {
//             await base.Launch();
//             _moving.Stop();
//             _observeArea.ActivateAttackCollider();
//
//             do
//             {
//                 if (_character.IsDead)
//                 {
//                     return new StateResult(_deathAIFactory.Create(_token), true);
//                 }
//                 
//                 if (!_observeArea.HasTarget)
//                 {
//                     break;
//                 }
//
//                 var lookDirection = new Vector3(
//                     _observeArea.TargetsTransforms.First().position.x - _character.transform.position.x, 0,
//                     _observeArea.TargetsTransforms.First().position.z - _character.transform.position.z);
//                 _character.LookDirection = lookDirection;
//                 _character.Fire();
//                 
//                 await UniTask.Yield();
//             }
//             while (!_token.IsCancellationRequested);
//             
//             _observeArea.DeactivateAttackCollider();
//             
//             return new StateResult(_pursueTargetAIFactory.Create(_token), true);
//         }
//
//         public class Factory : PlaceholderFactory<CancellationToken, AttackingAIState>
//         {
//         }
//     }
// }