using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class CresentPlayerHangState : PlayerHangState
    {
        private CresentPlayerRenderer _cresentPlayerRenderer;

        public CresentPlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _cresentPlayerRenderer = player.GetCompo<CresentPlayerRenderer>();
        }

        public override void Enter()
        {
            base.Enter();
            _cresentPlayerRenderer.SetSwingAttackDirectionVisualEnable(true);

        }

        public override void Exit()
        {
            base.Exit();
            Time.timeScale = 1f;

            _cresentPlayerRenderer.SetSwingAttackDirectionVisualEnable(false);

        }
    }
}