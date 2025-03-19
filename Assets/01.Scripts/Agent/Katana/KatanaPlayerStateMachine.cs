    using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerStateMachine : PlayerStateMachine
    {
        KatanaPlayerRenderer _katanaRenderer;
        public KatanaPlayerStateMachine(Player player) : base(player)
        {
            _katanaRenderer = _player.GetCompo<KatanaPlayerRenderer>();
        }

        public override void Initialize(string firstState)
        {
            AddState("Hang", "KatanaPlayerHang", playerRenderer.HangParam);

            AddState("Attack", "KatanaPlayerAttack", _katanaRenderer.AttackParam);
            AddState("AirAttack", "PlayerAirAttack", _katanaRenderer.AirAttackParam);


            base.Initialize(firstState);
        }
    }
}
