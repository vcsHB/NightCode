using Agents.Players;
using UnityEngine;
using Combat.Casters;

namespace Agents.Animate
{
    public class KatanaPlayerAnimationTrigger : PlayerAnimationTrigger
    {

        protected Player _player;
        [SerializeField] private Caster _swingAttackCaster;

        public override void Initialize(Agent agent)
        {
            _player = agent as Player;

        }

        public void CastSwingCaster()
        {
            _swingAttackCaster.Cast();
        }


    }


}
