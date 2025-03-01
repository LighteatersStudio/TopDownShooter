using UnityEngine;

namespace Gameplay.Projectiles
{
    public class SimpleExplosionAction : MonoBehaviour, IExplosionAction
    {
        [SerializeField] private float _radius = 1;

        private readonly Collider[] _hitColliders = new Collider[10];
        
        public void Blast(IFriendFoeSystem friendFoeSystem, IAttackInfo attackInfo)
        {
             var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _hitColliders);

             for (var i = 0; i < count; i++)
             {
                 if (!_hitColliders[i].TryGetComponent(out IDamageable damageable))
                 {
                      continue;   
                 }
                 
                 if (_hitColliders[i].TryGetComponent<FriendOrFoeComponent>(out var target))
                 {
                     if (friendFoeSystem.CheckFoes(attackInfo.FriendOrFoeTag, target))
                     {
                         damageable.TakeDamage(attackInfo);
                     }
                 }
             }
        }
    }
}