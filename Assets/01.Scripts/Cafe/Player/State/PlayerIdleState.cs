using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class PlayerIdleState : CafeEntityState
    {
        private CafePlayer _player;
        private CafeInput _input;

        public PlayerIdleState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as CafePlayer;
            _input = _player.input;
        }

        public override void UpdateState()
        {
            if (Mathf.Abs(_input.moveDir.x) > 0.5f)
                stateMachine.ChangeState("Move");
        }
    }
}
