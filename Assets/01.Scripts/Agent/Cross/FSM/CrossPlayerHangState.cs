using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CrossPlayerHangState : PlayerHangState
    {
        private CrossPlayerAnimationTrigger _crossAnimTrigger;
        public CrossPlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _crossAnimTrigger = _player.GetCompo<CrossPlayerAnimationTrigger>();
            
        }
        public override void UpdateState()
        {
            base.UpdateState();

        }

        protected override void HandleUseTurbo()
        {
            if (!_player.IsActive) return;
            if (!_canUseTurbo) return;

            ForceUseTurbo();
            _crossAnimTrigger.SetReload(true);
        }
    }
}