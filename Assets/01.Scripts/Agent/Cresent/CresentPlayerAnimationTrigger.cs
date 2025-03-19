using UnityEngine;
namespace Agents.Players
{

    public class CresentPlayerAnimationTrigger : PlayerAnimationTrigger
    {
        protected Player _player;


        public override void Initialize(Agent agent)
        {

            _player = agent as Player;            
        }
    }
}