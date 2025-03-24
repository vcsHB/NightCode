using Agents.Players;
using UnityEngine;
using Combat.Casters;

namespace Agents.Animate
{
    public class KatanaPlayerAnimationTrigger : PlayerAnimationTrigger
    {

        protected Player _player;
        [SerializeField] private Caster _swingAttackCaster;

        public void CastSwingGuard()
        {
            _swingAttackCaster.Cast();
        }
        public override void Initialize(Agent agent)
        {
            _player = agent as Player;

        }

        // public override void HandleAirAttack1()
        // {
        //     OnAirAttack1Event?.Invoke();
        //     ShootBlading();
        // }

        // public override void HandleAirAttack2()
        // {
        //     OnAirAttack2Event?.Invoke();
        //     ShootBlading();
        // }

        public void CastSwingCaster()
        {
            _swingAttackCaster.Cast();
        }


    }


}
