using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirBorneState : PlayerState
    {
        protected GrabController _grabThrower;
        protected PlayerAttackController _attackController;
        public PlayerAirBorneState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _grabThrower = player.GetCompo<GrabController>();
            _attackController = player.GetCompo<PlayerAttackController>();
            _canUseRope = true;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetGravityMultiplier(0.1f);
        }

        public override void Exit()
        {
            base.Exit();
            _mover.ResetGravityMultiplier();

        }


    }
}