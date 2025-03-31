using Combat;
using UnityEngine;
namespace Agents.Enemies
{

    public class GrabableEnemy : Enemy
    {
        public Transform GetTransform => transform;
        protected float _defaultGravity;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravity = RigidCompo.gravityScale;
        }

        // public virtual void Grab()
        // {
        //     RigidCompo.gravityScale = 0.1f;
        // }

        // public virtual void Release()
        // {
        //     RigidCompo.gravityScale = _defaultGravity;
        // }
    }
}