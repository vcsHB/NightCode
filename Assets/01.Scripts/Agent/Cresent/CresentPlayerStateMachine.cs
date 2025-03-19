using UnityEngine;
namespace Agents.Players.FSM
{


    public class CresentPlayerStateMachine : PlayerStateMachine
    {
        CresentPlayerRenderer _cresentRenderer;
        public CresentPlayerStateMachine(Player player) : base(player)
        {
            _cresentRenderer = player.GetCompo<CresentPlayerRenderer>();
        }

        public override void Initialize(string firstState)
        {

            AddState("Hang", "PlayerHang", playerRenderer.HangParam);


            base.Initialize(firstState);
        }
    }
}