using UnityEngine;
namespace Agents.Players.FSM
{

    public class CrossPlayerStateMachine : PlayerStateMachine
    {

        public CrossPlayerStateMachine(Player player) : base(player)
        {
        }

        public override void Initialize(string firstState)
        {
            AddState("Hang", "CrossPlayerHang", playerRenderer.HangParam);
            AddState("Swing", "PlayerSwing", playerRenderer.SwingParam);


            base.Initialize(firstState);
        }
    }
}