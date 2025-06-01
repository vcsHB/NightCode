using Agents;
using Agents.Players;
using UnityEngine;

namespace EffectSystem
{
    public class PlayerEffectController : AgentEffectController
    {
        private Player _player;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _player = _owner as Player;

        }

        public override void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
        {
            if(type == 0) return;
            
            base.ApplyEffect(type, duration, level);
            
        }
    }
}