using System.Collections.Generic;

namespace Combat.CombatObjects.ProjectileManage
{

    public interface IProjectileComponent
    {
        public void Initialize(Projectile projectile);
        public void OnGenerated();

        public void OnCasted();

        public void OnShot();
        
        public void OnCollision();

        public void OnProjectileDamaged();

        public void OnProjectileDestroy();
    }
}