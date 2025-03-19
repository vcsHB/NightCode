using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerPullState : PlayerAirBorneState
    {
        public PlayerPullState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canGrab = false;
            _canRemoveRope = false;
        }

        public override void Enter()
        {
            base.Enter();
            _grabThrower.PullTarget(HandleChangeGrab);
        }

        private void HandleChangeGrab()
        {
            
            _stateMachine.ChangeState("Grab");
        }
    }
}