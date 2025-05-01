using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerSlideDownState : PlayerWallState
    {
        public PlayerSlideDownState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.EventChannel.RaiseEvent(new FeedbackCreateEventData("SlideDown"));

        }


        public override void UpdateState()
        {
            float yInput = _player.PlayerInput.InputDirection.y;
            _mover.SetYMovement(yInput * 30f);
            if (yInput >= 0f)
            {
                _stateMachine.ChangeState("HoldingWall");
            }
            base.UpdateState();
        }

        public override void Exit()
        {
            base.Exit();
            _player.EventChannel.RaiseEvent(new FeedbackFinishEventData("SlideDown"));
        }
    }
}