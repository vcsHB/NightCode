using Agents.Animate;
using UnityEngine;

namespace Base
{
    public class PlayerIdleState : AvatarEntityState
    {
        private AvatarPlayer _player;
        private BaseInput _input;

        public PlayerIdleState(AvatarEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as AvatarPlayer;
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
