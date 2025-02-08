using UnityEngine;
namespace Combat.CombatObjects.ProjectileManage
{

    public class ProjectileReflector : MonoBehaviour, IProjectileComponent
    {
        void IProjectileComponent.Initialize(Projectile projectile)
        {
            throw new System.NotImplementedException();
        }

        void IProjectileComponent.OnCasted()
        {
            
        }

        void IProjectileComponent.OnGenerated()
        {
        }

        void IProjectileComponent.OnProjectileDamaged()
        {
        }

        void IProjectileComponent.OnProjectileDestroy()
        {

        }

        void IProjectileComponent.OnShot()
        {
            
        }
    }
}