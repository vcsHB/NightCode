using Combat.Casters;
using UnityEngine;
namespace Agents.Players
{

    public class CresentPlayerAnimationTrigger : PlayerAnimationTrigger
    {
        protected Player _player;
        [SerializeField] private Caster _swingSlashCaster;


        public override void Initialize(Agent agent)
        {

            _player = agent as Player;            
        }

        public void CastSwingAttackCaster()
        {
            _swingSlashCaster.Cast();
        }
    }
}