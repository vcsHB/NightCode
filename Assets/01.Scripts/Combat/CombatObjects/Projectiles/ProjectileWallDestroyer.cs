using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileWallDestroyer : MonoBehaviour, IProjectileComponent
    {
        protected Projectile _owner;
        public virtual void Initialize(Projectile projectile)
        {
            _owner = projectile;
        }

        public virtual void OnCasted() { }

        public virtual void OnCollision()
        {
            _owner.HandleDestroy();
        }

        public virtual void OnGenerated() { }

        public virtual void OnProjectileDamaged() { }

        public virtual void OnProjectileDestroy() { }

        public virtual void OnShot() { }
    }
}