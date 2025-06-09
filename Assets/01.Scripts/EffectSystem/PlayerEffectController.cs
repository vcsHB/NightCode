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

        public override void ApplyEffect(EffectStateTypeEnum type, int level, int stack, float percent = 1)
        {
            if (type == 0) return;
            base.ApplyEffect(type, level, stack, percent);
        }
    }
}