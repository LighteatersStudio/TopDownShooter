using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    public class ProjectilePool : MonoBehaviour
    {
        private Projectile.Factory _projectileFactory;
        private List<Projectile> _projectiles;

        [Inject]
        public void Construct(Projectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            _projectiles = new List<Projectile>();
        }

        public void AddProjectile(FlyInfo flyInfo, AttackInfo attackInfo)
        {
            var projectile = _projectileFactory.Create(flyInfo, attackInfo);
            projectile.Launch();

            _projectiles.Add(projectile);
        }

        public void RemoveProjectile(Projectile projectile)
        {
            if (_projectiles.Any())
            {
                projectile = _projectiles[0];
                projectile.Dispose();
                _projectiles.Remove(projectile);
            }
        }
    }
}