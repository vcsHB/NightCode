using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAttackableState : PlayerGroundState
    {
        public PlayerAttackableState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
        }

        public override void Enter()
        {
            _player.PlayerInput.OnAttackEvent += HandleAttack;
            base.Enter();
        }


        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnAttackEvent -= HandleAttack;

        }

        
        private void HandleAttack()
        {
            if (!_player.IsActive) return;
            _stateMachine.ChangeState("Attack");
        }
    }
}