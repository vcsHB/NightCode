using Agents.Players;
using UnityEngine.Events;
using UnityEngine;
using Combat.CombatObjects.ProjectileManage;
using Combat.Casters;

namespace Agents.Animate
{
    public class KatanaPlayerAnimationTrigger : PlayerAnimationTrigger
    {
        private AimController _aimController;
        [SerializeField] private ProjectileShooter _airAttackShooter;
        public UnityEvent OnAirAttack1Event;
        public UnityEvent OnAirAttack2Event;
        protected Player _player;
        [SerializeField] private Caster _swingAttackCaster;


        public override void Initialize(Agent agent)
        {
            _player = agent as Player;
            _aimController = _player.GetCompo<AimController>();

        }

        public override void HandleAirAttack1()
        {
            OnAirAttack1Event?.Invoke();
            ShootBlading();
        }

        public override void HandleAirAttack2()
        {
            OnAirAttack2Event?.Invoke();
            ShootBlading();
        }

        private void ShootBlading()
        {
            _airAttackShooter.SetDirection(_aimController.AimDirection);
            _airAttackShooter.FireProjectile();
        }

        public void CastSwingCaster()
        {
            _swingAttackCaster.Cast();
        }

        
    }


}
