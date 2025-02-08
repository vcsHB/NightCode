using System.Collections.Generic;

namespace Combat.CombatObjects.ProjectileManage
{

    public interface IProjectileComponent
    {
        public void Initialize(Projectile projectile);
        internal void OnGenerated();

        internal void OnCasted();

        internal void OnShot();

        internal void OnProjectileDamaged();

        internal void OnProjectileDestroy();
    }
}