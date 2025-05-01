using Agents.Animate;
using UnityEngine;

namespace Base
{
    public class PlayerIdleState : BaseEntityState
    {
        private BasePlayer _player;
        private BaseInput _input;

        public PlayerIdleState(BaseEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as BasePlayer;
            _input = _player.input;
        }

        public override void EnterState()
        {
            base.EnterState();
            _player.StopImmediatly();
        }

        public override void UpdateState()
        {
            if (Mathf.Abs(_input.MoveDir.x) > 0.5f)
                stateMachine.ChangeState("Move");
        }
    }
}
