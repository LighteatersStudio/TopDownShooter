// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
// using Zenject;
//
// namespace Gameplay.AI
// {
//     public class PatrolAIState : CharacterDeathStateHandler
//     {
//         private readonly NavMeshMoving _moving;
//         private readonly MovingPath _path;
//         private readonly CancellationToken _token;
//         private readonly ObserveArea _observeArea;
//         private readonly IdleAIState.Factory _factory;
//         private readonly AttackingAIState.Factory _attackingAIFactory;
//         private readonly DeathAIState.Factory _deathAIFactory;
//         private readonly Character _character;
//
//         public PatrolAIState(NavMeshMoving moving,
//             MovingPath path,
//             IdleAIState.Factory factory,
//             CancellationToken token,
//             ObserveArea observeArea,
//             AttackingAIState.Factory attackingAIFactory,
//             Character character,
//             DeathAIState.Factory deathAIFactory) : base(token, character, factory)
//         {
//             _moving = moving;
//             _path = path;
//             _factory = factory;
//             _token = token;
//             _observeArea = observeArea;
//             _attackingAIFactory = attackingAIFactory;
//             _character = character;
//             _deathAIFactory = deathAIFactory;
//         }
//
//         public override async Task<IAIState> Launch()
//         {
//             await base.Launch();
//             var path = _path;
//
//             do
//             {
//                 if (_character.IsDead)
//                 {
//                     return new StateResult(_deathAIFactory.Create(_token), true);
//                 }
//
//                 if (_observeArea.HasTarget)
//                 {
//                     return new StateResult(_attackingAIFactory.Create(_token), true);
//                 }
//
//                 await MoveThroughPath(path.Points, InternalSource.Token);
//                 await UniTask.Yield();
//
//                 path = _path.Reverse();
//             } while (!_token.IsCancellationRequested);
//
//             return new StateResult(_factory.Create(_token), false);
//         }
//
//         private async Task MoveThroughPath(IEnumerable<Vector3> points, CancellationToken token)
//         {
//             var internalToken = GetTargetChangedToken(token);
//
//             foreach (var pathPoint in points)
//             {
//                 if (internalToken.IsCancellationRequested || _observeArea.HasTarget)
//                 {
//                     break;
//                 }
//
//                 if (!await _moving.MoveTo(pathPoint, internalToken) && !internalToken.IsCancellationRequested)
//                 {
//                     Debug.LogError($"MovePoint NOT REACHED: {pathPoint}");
//                 }
//             }
//         }
//
//         private CancellationToken GetTargetChangedToken(CancellationToken parentToken)
//         {
//             CancellationTokenSource internalSource = new CancellationTokenSource();
//             parentToken.Register(() => internalSource.Cancel());
//
//             var internalToken = internalSource.Token;
//
//             _observeArea.TargetsChanged += OnTargetChanged;
//
//             void OnTargetChanged()
//             {
//                 if (!_observeArea.HasTarget)
//                 {
//                     return;
//                 }
//
//                 _observeArea.TargetsChanged -= OnTargetChanged;
//                 internalSource.Cancel();
//             }
//
//             return internalToken;
//         }
//
//         public class Factory : PlaceholderFactory<CancellationToken, PatrolAIState>
//         {
//         }
//     }
// }