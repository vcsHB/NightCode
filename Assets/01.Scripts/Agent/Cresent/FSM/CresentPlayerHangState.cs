using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CresentPlayerHangState : PlayerHangState
    {
        private CresentPlayerRenderer _cresentPlayerRenderer;
        private CresentPlayerAnimationTrigger _cresentAnimTrigger;

        public CresentPlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _cresentPlayerRenderer = player.GetCompo<CresentPlayerRenderer>();
            _cresentAnimTrigger = player.GetCompo<CresentPlayerAnimationTrigger>();
            
        }

        public override void Enter()
        {
            base.Enter();
            if(_aimController.IsGrabTargeted)
                HandlePull();
            //_cresentPlayerRenderer.SetSwingAttackDirectionVisualEnable(true);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _cresentAnimTrigger.CastSwingAttackCaster();
            
        }

        public override void Exit()
        {
            base.Exit();

        }


        protected override void HandleRemoveRope()
        {
            base.HandleRemoveRope();
        }
    }
}